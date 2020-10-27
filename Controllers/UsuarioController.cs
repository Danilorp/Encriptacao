using Encriptacao.Context;
using Encriptacao.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Encriptacao.Controllers
{
    public class UsuarioController : Controller
    {

        private Contexto db = new Contexto();
        private static string AesIV256BD = @"%j?TmFP6$BbMnY$@";//16 caracteres
        private static string AesKey256BD = @"rxmBUJy]&,;3jKwDTzf(cui$<nc2EQr)";//32 caracteres


        #region Index

        // GET: Usuario
        public ActionResult Index()
        {
            return View(db.Usuarios.ToList());
        }
        #endregion

        #region Create - GET
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        #endregion

        #region Create - POST
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(Models.UsuarioModel usuarioModel)
        {
            if (ModelState.IsValid)
            {
                // Hash de Senha
                usuarioModel.Senha = BCrypt.Net.BCrypt.HashPassword(usuarioModel.Senha);
                usuarioModel.ConfirmeSenha = usuarioModel.Senha;

                //AesCryptoServiceProvider
                AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                aes.BlockSize = 128;
                aes.KeySize = 256;
                aes.IV = Encoding.UTF8.GetBytes(AesIV256BD);
                aes.Key = Encoding.UTF8.GetBytes(AesKey256BD);
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                // Convertendo string para byte array
                byte[] src = Encoding.Unicode.GetBytes(usuarioModel.Email);

                //Encriptação
                using (ICryptoTransform encrypt = aes.CreateEncryptor())
                {
                    byte[] dest = encrypt.TransformFinalBlock(src, 0, src.Length);

                    // Converte byte array para string de base 64
                    usuarioModel.Email = Convert.ToBase64String(dest);
                }
                db.Usuarios.Add(usuarioModel);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(usuarioModel);
        }
        #endregion

    }
}