namespace Company.Shorts.Domain
{
    using Company.Shorts.Domain.Seedwork;

    public class FileExtension : Enumeration
    {
        public FileExtension(int id, string name, List<byte[]> signatures) : base(id, name)
        {
            this.Signatures = signatures;
        }
        public List<byte[]> Signatures { get; }

       

        private static readonly FileExtension gif = new(
            1,
            ".gif",
            new List<byte[]> { new byte[] { 0x47, 0x49, 0x46, 0x38 } }
        );

        private static readonly FileExtension png = new(
            2,
            ",png",
            new List<byte[]> { new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A } }
        );

        private static readonly FileExtension jpeg = new(
            3,
            ".jpeg",
            new List<byte[]>
            {
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
            }
        );

        private static readonly FileExtension jpg = new(
            4,
            ".jpg",
            new List<byte[]>
            {
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 },
            }
        );

        private static readonly FileExtension txt = new(
            5,
            ".txt",
            new List<byte[]> { }
        );

        public static FileExtension Gif { get; } = gif;

        public static FileExtension Png { get; } = png;

        public static FileExtension Jpeg { get; } = jpeg;

        public static FileExtension Jpg { get; } = jpg;

        public static FileExtension Txt { get; } = txt;
    }
}
