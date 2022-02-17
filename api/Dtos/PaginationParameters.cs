using System;
using System.Collections.Generic;
using static Lapis.API.Helpers.CommonEnum;

namespace Lapis.API.Dtos
{
    public class PaginationParameters
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 25;
        private HashSet<string> _tags = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        public HashSet<string> Tags
        {
            get { return _tags; }
            set
            {
                if (value == null) _tags = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                else _tags = value;
            }
        }

        private string _orientation = "";
        public string Orientation
        {
            get { return _orientation; }
            set
            {
                switch (value)
                {
                    case nameof(ImageOrientation.Horizontal):
                        _orientation = ImageOrientation.Horizontal.ToString();
                        break;
                    case nameof(ImageOrientation.Vertical):
                        _orientation = ImageOrientation.Vertical.ToString();
                        break;
                    case nameof(ImageOrientation.Square):
                        _orientation = ImageOrientation.Square.ToString();
                        break;
                    default:
                        _orientation = "";
                        break;
                }
            }
        }

        private string _primaryColor = "";
        public string PrimaryColor
        {
            get { return _primaryColor; }
            set
            {
                if (value is null) _primaryColor = "";
                else _primaryColor = value;
            }
        }

        private string _secondaryColor = "";
        public string SecondaryColor
        {
            get { return _secondaryColor; }
            set
            {
                if (value is null) _primaryColor = "";
                else _secondaryColor = value;
            }
        }

        private string _tertiaryColor = "";
        public string TertiaryColor
        {
            get { return _tertiaryColor; }
            set
            {
                if (value is null) _primaryColor = "";
                else _tertiaryColor = value;
            }
        }
    }

    public class PaginationResponse<T>
    {
        public long TotalRecord { get; set; } = 0;
        public T Payload { get; set; }

        public PaginationResponse() { }

        public PaginationResponse(long totalRecord, T payload)
        {
            this.TotalRecord = totalRecord;
            this.Payload = payload;
        }
    }
}