using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using InvalidCastException = System.InvalidCastException;
using Timer = System.Threading.Timer;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;

namespace TRRP1
{
    public partial class MainForm : Form
    {
        private bool auth;
        private string rToken;
        private string token;
        private readonly Dictionary<string, string> applicationData;
        private List<Photo> photos;
        private int curPhoto = -1;
        private string credPath = @"token.json"; // путь к файлу с токеном
        private string tokenPath = @"_token.json"; // путь к json файлу с токеном

        private bool Auth
        {   
            get => auth;
            set
            {
                auth = value;
                UpdateButton();
            }
        }
        private static readonly byte[] key = Encoding.ASCII.GetBytes("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        private static readonly byte[] iv = Encoding.ASCII.GetBytes("bbbbbbbbbbbbbbbb");

        public struct Item {
            public Dictionary<string, string> installed;
        };

        public MainForm()
        {
            InitializeComponent();
            lbUserName.Text = "Вы не авторизованы!";
            Auth = false;
            pbMain.SizeMode = PictureBoxSizeMode.Zoom;

            Item tempData = JsonConvert.DeserializeObject<Item>(
                    new StreamReader(@"C:\Users\dimas\Desktop\10 TRIMESTR\ТРРП\TRRP1\TRRP1\credentials.json").ReadToEnd());

            applicationData = tempData.installed; 

            new Timer(RefreshToken, null, 1000 * 60 * 50, Timeout.Infinite);
        }

        #region Обработка кнопок


        private void btLogin_Click(object sender, EventArgs e)
        {
            string[] Scopes = { "https://www.googleapis.com/auth/photoslibrary" };

            UserCredential credential = null;

            using (var stream = new FileStream(@"C:\Users\dimas\Desktop\10 TRIMESTR\ТРРП\TRRP1\TRRP1\credentials.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            string json = JsonConvert.SerializeObject(credential.Token);
            File.WriteAllText(tokenPath, json);

            InitToken();
        }

        private void btLogout_Click(object sender, EventArgs e)
        {
            //удаление token файлов
            File.Delete(tokenPath);
            Directory.Delete(credPath, true);
            token = null;
            rToken = null;

            photos = null;
            curPhoto = -1;
            LoadPhoto();
            Auth = false;
        }

        private void btCreate_Click(object sender, EventArgs e)
        {
            fdLoad.Filter = @"Image files (*.BMP; *.GIF; *.HEIC; *.ICO; *.JPG; *.PNG; *.TIFF; *.WEBP)|*.BMP; *.GIF; *.HEIC; *.ICO; *.JPG; *.PNG; *.TIFF; *.WEBP";
            fdLoad.ShowDialog();
            if (!File.Exists(fdLoad.FileName))
            {
                MessageBox.Show("File not exists", "Error!!!");
                return;
            }

            var fs = new FileStream(fdLoad.FileName, FileMode.Open, FileAccess.Read);
            var body = new byte[fs.Length];
            fs.Read(body, 0, (int)fs.Length);
            var fileName = fdLoad.FileName.Substring(fdLoad.FileName.LastIndexOf('\\') + 1);

            var header = new WebHeaderCollection();
            try
            {
                header["X-Goog-Upload-File-Name"] = fileName;
            }
            catch
            {
                header["X-Goog-Upload-File-Name"] = "default";
            }
            header["X-Goog-Upload-Protocol"] = "raw";
            this.Enabled = false;
            try
            {
                var answer = sendRequest("https://photoslibrary.googleapis.com/v1/uploads",
                    "application/octet-stream",
                    true,
                    WebRequestMethods.Http.Post, body, header);

                JObject jbody = new JObject();
                JObject mediaItem = new JObject();
                JObject simpleMediaItem = new JObject();
                simpleMediaItem["uploadToken"] = answer;
                mediaItem["description"] = "";
                mediaItem["simpleMediaItem"] = simpleMediaItem;
                jbody["newMediaItems"] = new JArray(mediaItem);

                sendRequest("https://photoslibrary.googleapis.com/v1/mediaItems:batchCreate",
                    "application/json",
                    true,
                    WebRequestMethods.Http.Post,
                    Encoding.Default.GetBytes(jbody.ToString()));

                this.Enabled = true;
                btRead.PerformClick();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error!!!");
            }
            finally
            {
                this.Enabled = true;
                this.TopMost = true;
            }
        }

        private void btRead_Click(object sender, EventArgs e)
        {
            var site = "https://photoslibrary.googleapis.com/v1/mediaItems";

            JObject answer;
            try
            {
                this.Enabled = false;
                answer = sendRequest<JObject>(site, "application/json", true);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Erorr!!!");
                return;
            }
            finally
            {
                this.Enabled = true;
            }

            var a = new JArray(answer["mediaItems"].Children());

            photos = new List<Photo>();

            foreach (var item in a)
            {
                photos.Add(new Photo(item));
            }

            if (photos.Count > 0)
                curPhoto = 0;

            LoadPhoto();
        }

        #endregion

        #region Второстепенные функции

        private void UpdateButton()
        {
            if (curPhoto == -1)
            {
                btPrev.Enabled = false;
                btNext.Enabled = false;
            }
            else
            {
                btPrev.Enabled = btNext.Enabled = true;
                if (curPhoto == 0)
                    btPrev.Enabled = false;
                if (curPhoto == photos.Count - 1)
                    btNext.Enabled = false;
            }

            btCreate.Enabled = btRead.Enabled = btLogout.Enabled = Auth;
            btLogin.Enabled = !Auth;
            lbUserName.Text = Auth ? @"Вы авторизованы :)" : @"Вы не авторизованы!";
        }

        private void LoadPhoto()
        {
            if (photos == null || photos.Count == 0)
            {
                curPhoto = -1;
                pbMain.Image = null;
            }
            else
            {
                if (curPhoto >= photos.Count)
                    curPhoto--;
                if (curPhoto <= -1)
                    curPhoto++;
                pbMain.Load(photos[curPhoto].baseUrl);
                UpdateButton();
            }

        }

        private void btPrev_Click(object sender, EventArgs e)
        {
            curPhoto--;
            LoadPhoto();
        }

        private void btNext_Click(object sender, EventArgs e)
        {
            curPhoto++;
            LoadPhoto();
        }

        #endregion

        #region Обработка и получение токенов

        private void InitToken()
        {
            using (var reader = new StreamReader(tokenPath))
            {
                var tokenInfo = JsonConvert.DeserializeObject<Dictionary<string, string>>(reader.ReadToEnd());

                token = tokenInfo["access_token"].ToString();
                rToken = tokenInfo["refresh_token"].ToString();
                Auth = true;
            }            
        }

        private void RefreshToken(object state)
        {
            if (string.IsNullOrEmpty(rToken))
                return;

            var site = "https://oauth2.googleapis.com/token";
            var bodyRequest = $"client_id={applicationData["client_id"]}" +
                              $"&client_secret={applicationData["client_secret"]}" +
                              $"&refresh_token={rToken}" +
                              $"&grant_type=refresh_token";

            try
            {
                var answer = sendRequest<JObject>(
                    site,
                    "application/x-www-form-urlencoded",
                    false,
                    WebRequestMethods.Http.Post,
                    Encoding.Default.GetBytes(bodyRequest));

                token = answer["access_token"].ToString();
                Auth = true;
            }
            catch (Exception e)
            {
                Auth = false;
                MessageBox.Show(e.Message, @"Error!!!");
            }

        }

        private string EncodingToken(string token)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(token);
                        }
                        string answer = "";
                        answer = Convert.ToBase64String(msEncrypt.ToArray());
                        return answer;
                    }
                }
            }
        }

        private string DecodingToken(string token)
        {
            string answer = "";
            using (Aes aesAlg = Aes.Create())
            {
                if (aesAlg == null) throw new Exception();
                aesAlg.Key = key;
                aesAlg.IV = iv;
                byte[] byteToken = Convert.FromBase64String(token);
                using (var msDecrypt = new MemoryStream(byteToken))
                {
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            answer = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return answer;
        }

        #endregion

        private T sendRequest<T>(string site,
            string contentType = null,
            bool authorization = false,
            string method = WebRequestMethods.Http.Get,
            byte[] body = null,
            WebHeaderCollection header = null)
        {
            return JsonConvert.DeserializeObject<T>(sendRequest(site, contentType, authorization, method, body, header));
        }

        private string sendRequest(string site,
            string contentType = null,
            bool authorization = false,
            string method = WebRequestMethods.Http.Get,
            byte[] body = null,
            WebHeaderCollection header = null)
        {
            var request = HttpWebRequest.Create(site);

            if (header != null)
                request.Headers = header;

            request.Method = method;

            if (body != null)
            {
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(body, 0, body.Length);
                    stream.Flush();
                    stream.Close();
                }
            }

            if (authorization)
                request.Headers[HttpRequestHeader.Authorization] = "Bearer " + token;
            
            if (contentType != null)
                request.ContentType = contentType;
            var response = (HttpWebResponse) request.GetResponse();

            string answerJson;

            using (var reader = new StreamReader(response.GetResponseStream(), Encoding.ASCII))
            {
                answerJson = reader.ReadToEnd();
            }

            return answerJson;
        }
    }
}
