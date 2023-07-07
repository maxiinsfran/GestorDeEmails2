﻿using System.Security.Cryptography;
using System.Text;

namespace GestorDeEmails2.Recursos
{
    public class Utilidades
    {
        public static string EncriptarContrasenia(string contrasenia)
        {
            StringBuilder sb = new StringBuilder();

            using(SHA256 hash  = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;

                byte[] result = hash.ComputeHash(enc.GetBytes(contrasenia));

                foreach(byte b in result)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}
