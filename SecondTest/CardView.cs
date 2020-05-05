using System;
using UIKit;

namespace SecondTest
{
    public class CardView: UIView
    {
        UILabel theLabel;

        public CardView()
        {
            Initialize();
        }
        public CardView(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        void Initialize()
        {
            theLabel = new UILabel();
            this.AddSubview(theLabel);
            theLabel.Text = "Testing";
            theLabel.Lines = 0;
            theLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            theLabel.TopAnchor.ConstraintEqualTo(this.TopAnchor, 8.0f).Active = true;
            theLabel.LeadingAnchor.ConstraintEqualTo(this.LeadingAnchor, 8.0f).Active = true;
            theLabel.TrailingAnchor.ConstraintEqualTo(this.TrailingAnchor, -8.0f).Active = true;
            theLabel.BottomAnchor.ConstraintEqualTo(this.BottomAnchor, -8.0f).Active = true;
        }

        public void fillCard(String s)
        {
            this.theLabel.Text = s;
        }

    }
}
