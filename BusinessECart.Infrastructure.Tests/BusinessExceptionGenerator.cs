using System.Reflection;
using BusinessECart.Contracts.BassClass;
using BusinessECart.Service;
using BusinessECart.Service.Authentications.Authentication.Contracts.Exceptions;
using Xunit;

namespace BusinessECart.Infrastructure.Tests
{
    public class BusinessExceptionGenerator
    {
        [Fact]
        public void ExceptionGenerator()
        {
            var assembly = Assembly.GetAssembly(typeof(ServiceExceptionAssembly));
            var exceptionTypes = assembly!.GetTypes();

            string strException = string.Empty;

            foreach (var type in exceptionTypes)
            {
                if (type.IsSubclassOf(typeof(BusinessException)))
                {
                    strException += $"\"{type.Name.Replace("Exception", "")}\": " +
                                    "\"Translate Exception here\"," +
                                    Environment.NewLine;
                }
            }

            strException += "\"UnknownError\": " +
                            "\"خطایی رخ داده است، با پشتیبانی تماس بگیرید\"";

            // ✅ مسیر دلخواه شما:
            var outputPath = @"C:\Users\Alireza\Desktop\Exception-translate\GeneratedExceptions.json";

            // اطمینان از وجود پوشه
            var directory = Path.GetDirectoryName(outputPath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory!);

            File.WriteAllText(outputPath, strException);
        }
        }
    
}