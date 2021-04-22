using CommandLine;
using EncryptDecryptManager.Utility;
using System;
using System.Security.Cryptography;

namespace EncryptDecryptManager
{

    class Program
    {
        [Verb("encrypt", HelpText = "Encrypt given string using the public key provided.")]
        class EncryptOption
        {
            [Option("key", Required = true, HelpText = "Public key for encryption.")]
            public string Key { get; set; }
            [Option("text", Required = true, HelpText = "Text to encrypt.")]
            public string Text { get; set; }
        }

        [Verb("decrypt", HelpText = "Decrypt given string using the public key provided.")]
        class DecryptOption
        {
            [Option("key", Required = true, HelpText = "Public key for decryption.")]
            public string Key { get; set; }
            [Option("text", Required = true, HelpText = "Text to decrypt.")]
            public string Text { get; set; }
        }

        static int Main(string[] args)
        {
            
            return Parser.Default.ParseArguments<EncryptOption, DecryptOption>(args)
                .MapResult(
                    (EncryptOption o) => RunEncrypt(o),
                    (DecryptOption o) => RunDencrypt(o),
                    errs => 1);
        }


        private static int RunEncrypt(EncryptOption o)
        {
            var s = Helper.EncryptString(o.Key, o.Text);
            Console.WriteLine("Encrypted text : " + Helper.EncryptString(o.Key, o.Text));
            return 0;
        }

        private static int RunDencrypt(DecryptOption o)
        {
            Console.WriteLine("Decrypted text : " + Helper.DecryptString(o.Key, o.Text));
            return 0;
        }
    }
}
