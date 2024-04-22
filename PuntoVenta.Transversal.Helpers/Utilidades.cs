namespace PuntoVenta.Transversal.Helpers
{
	public static class Utilidades
	{
        public static (byte[], byte[]) CreatePasswordHash(string password)
        {
            byte[] passwordHash, passwordSalt;
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

            return (passwordHash, passwordSalt);
        }

        public static bool VerififyPasswordHash(string password, byte[]? passwordHash, byte[]? passwordSalt)
		{
			if (passwordHash != null && passwordSalt != null)
			{
				using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
				{
					var hashComputado = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

					for (int i = 0; i < hashComputado.Length; i++)
					{
						if (hashComputado[i] != passwordHash[i]) return false;
					}
				}
			}
			
			return true;
		}
	}
}