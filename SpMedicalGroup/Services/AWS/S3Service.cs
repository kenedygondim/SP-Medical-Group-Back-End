using Amazon.S3;
using Amazon.S3.Transfer;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SpMedicalGroup.Services.AWS
{
    public class S3Service
    {
        private readonly string _bucketName = "sp-medical-group";
        private readonly string _region = "us-east-1"; // Substitua pela região do seu bucket

        private readonly IAmazonS3 _s3Client;

        public S3Service()
        {
            // O AWS SDK usará automaticamente o arquivo de credenciais padrão
            try
            {
                _s3Client = new AmazonS3Client(Amazon.RegionEndpoint.GetBySystemName(_region));

                if (_s3Client == null)
                {
                    throw new Exception("erro");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName)
        {
            try
            {
                var transferUtility = new TransferUtility(_s3Client);

                var uploadRequest = new TransferUtilityUploadRequest
                {
                    InputStream = fileStream,
                    Key = fileName,
                    BucketName = _bucketName,
                    ContentType = "image/jpeg"
                };

                await transferUtility.UploadAsync(uploadRequest);

                return $"https://{_bucketName}.s3.{_region}.amazonaws.com/{fileName}";
            }
            catch (Exception ex)
            {
                // Log ou tratamento de erro
                throw new Exception("Erro ao fazer upload do arquivo para o S3", ex);
            }
        }
    }
}
