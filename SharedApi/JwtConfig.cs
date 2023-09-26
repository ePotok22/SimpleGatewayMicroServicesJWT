namespace SharedApi
{
    public static class JwtConfig
    {
        // Token Validation Parameters
        public const string TokenSecurityKey = "secretJWTsigningKey@1234567890#TestKey";
        public const bool TokenValidateIssuer = true;
        public const bool TokenValidateAudience = false;
        public const bool TokenValidateIssuerSigningKey = true;
        // Jwt Bearer
        public const bool SaveToken = true;
        public const bool RequireHttpsMetadata = false;

    }
}
