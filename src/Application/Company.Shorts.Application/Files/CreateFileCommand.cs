namespace Company.Shorts.Application.Files
{
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CreateFileCommand : IRequest<Guid>
    {
        public CreateFileCommand(byte[] data, string contentType, string contentDisposition)
        {
            Data = data;
            ContentType = contentType;
            ContentDisposition = contentDisposition;
        }

        public byte[] Data { get; set; }

        public string ContentType { get; set; }

        public string ContentDisposition { get; set; }
    }
}
