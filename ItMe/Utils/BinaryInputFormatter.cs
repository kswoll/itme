using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace ItMe.Utils
{
    public class BinaryInputFormatter : InputFormatter
    {
        private const string binaryContentType = "application/octet-stream";
//        private const int bufferLength = 16384;

        public BinaryInputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse(binaryContentType));
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
//            using (MemoryStream ms = new MemoryStream(bufferLength))
//            {
//                await context.HttpContext.Request.Body.CopyToAsync(ms);
//                object result = ms.ToArray();
            return await InputFormatterResult.SuccessAsync(context.HttpContext.Request.Body);
//            }
        }

        protected override bool CanReadType(Type type)
        {
            if (type == typeof(Stream))
                return true;
            else
                return false;
        }
    }
}