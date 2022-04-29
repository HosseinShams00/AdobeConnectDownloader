using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AdobeConnectDownloader.UI
{
    public partial class DesignComponent : Component
    {
        public DesignComponent()
        {
            InitializeComponent();
        }

        public DesignComponent(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private Region ButtonRegion { get; set; }

        private bool _upLeft { get; set; }

        [Category("Initialize Values")]
        public bool UpLeft
        {
            get { return _upLeft; }
            set
            {
                if (_Button != null)
                {
                    _Button.Region = Helper.Design.GetRoundRegion(value, UpRight, LowRight, LowLeft, _Button.Size, XRadius, YRadius);
                    _upLeft = value;
                }
            }
        }

        private bool _upRight { get; set; }
        [Category("Initialize Values")]

        public bool UpRight
        {
            get { return _upRight; }
            set
            {
                if (_Button != null)
                {
                    _Button.Region = Helper.Design.GetRoundRegion(UpLeft, value, LowRight, LowLeft, _Button.Size, XRadius, YRadius);
                    _upRight = value;
                }

            }
        }

        private bool _lowRight { get; set; }
        [Category("Initialize Values")]

        public bool LowRight
        {
            get { return _lowRight; }

            set
            {
                _Button.Region = Helper.Design.GetRoundRegion(UpLeft, UpRight, value, LowLeft, _Button.Size, XRadius, YRadius);
                _lowRight = value;
            }
        }

        private bool _lowLeft { get; set; }
        [Category("Initialize Values")]

        public bool LowLeft
        {
            get { return _lowLeft; }

            set
            {
                _Button.Region = Helper.Design.GetRoundRegion(UpLeft, UpRight, LowRight, value, _Button.Size, XRadius, YRadius);
                _lowLeft = value;
            }
        }

        private float _XRadius { get; set; } = 5;
        [Category("Initialize Values")]

        public float XRadius
        {
            get { return _XRadius; }
            set
            {
                _Button.Region = Helper.Design.GetRoundRegion(UpLeft, UpRight, LowRight, LowLeft, _Button.Size, value, YRadius);
                _XRadius = value;

            }
        }

        private float _YRadius { get; set; } = 5;

        [Category("Initialize Values")]

        public float YRadius
        {
            get { return _YRadius; }
            set
            {
                _Button.Region = Helper.Design.GetRoundRegion(UpLeft, UpRight, LowRight, LowLeft, _Button.Size, XRadius, value);
                _YRadius = value;

            }
        }


        private Button _Button { get; set; }
        [Category("Button For Set Style")]

        public Button _SetButton
        {
            get
            {
                return _Button;
            }
            set
            {
                if (value != null)
                {
                    if (_Button != null)
                    {
                        _Button.Region = ButtonRegion;
                        _Button = value;
                        ButtonRegion = value.Region;
                    }
                    else
                    {
                        _Button = value;
                        ButtonRegion = value.Region;
                    }

                }
                else
                {
                    if (_Button != null)
                    {
                        _Button = null;
                        ButtonRegion = null;
                    }
                    else
                    {
                        _Button.Region = ButtonRegion;
                        _Button = null;
                        ButtonRegion = null;
                    }
                }

            }

        }

        private string _BackGroundHtmlColorCode { get; set; } = ColorTranslator.ToHtml(Color.Black);
        [Category("Colors")]
        public string BackGroundHtmlColorCode
        {
            get { return _BackGroundHtmlColorCode; }
            set
            {
                if (_Button != null)
                {
                    _Button.BackColor = ColorTranslator.FromHtml(value);
                    _BackGroundHtmlColorCode = value;
                }
            }
        }

        private string _ForeGroundHtmlColorCode { get; set; } = ColorTranslator.ToHtml(Color.White);
        [Category("Colors")]
        public string ForeGroundHtmlColorCode
        {
            get { return _ForeGroundHtmlColorCode; }
            set
            {
                if (_Button != null)
                {
                    _Button.ForeColor = ColorTranslator.FromHtml(value);
                    _ForeGroundHtmlColorCode = value;
                }
            }
        }


    }
}
