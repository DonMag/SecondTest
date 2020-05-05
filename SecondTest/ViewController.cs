using Foundation;
using System;
using UIKit;

namespace SecondTest
{
    public partial class ViewController : UIViewController
    {

        UIScrollView _scrollView;
        UIRefreshControl _refreshControl;
        UIStackView _stackView;

        CardView qolCard;
        CardView goalsCard;

        int iCardCounter = 0;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            _scrollView = new UIScrollView
            {
                ShowsHorizontalScrollIndicator = false,
                TranslatesAutoresizingMaskIntoConstraints = false
            };

            if (!UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                _scrollView.AlwaysBounceVertical = true;
                _scrollView.Bounces = true;
            }

            _refreshControl = new UIRefreshControl { TranslatesAutoresizingMaskIntoConstraints = false };
            _refreshControl.Enabled = true;
            _refreshControl.ValueChanged -= RefreshControl_ValueChanged;
            _refreshControl.ValueChanged += RefreshControl_ValueChanged;
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                _scrollView.RefreshControl = _refreshControl;
            }
            else
            {
                _scrollView.AddSubview(_refreshControl);
            }

            // 1. Add scrollview to VC view
            this.View.AddSubview(_scrollView);

            // 2. constrain all 4 sides to safe area
            _scrollView.TopAnchor.ConstraintEqualTo(this.View.SafeAreaLayoutGuide.TopAnchor, 0f).Active = true;
            _scrollView.LeadingAnchor.ConstraintEqualTo(this.View.SafeAreaLayoutGuide.LeadingAnchor, 0f).Active = true;
            _scrollView.TrailingAnchor.ConstraintEqualTo(this.View.SafeAreaLayoutGuide.TrailingAnchor, 0f).Active = true;
            _scrollView.BottomAnchor.ConstraintEqualTo(this.View.SafeAreaLayoutGuide.BottomAnchor, 0f).Active = true;

            // 3. add a UIStackView to scroll view
            _stackView = new UIStackView
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Axis = UILayoutConstraintAxis.Vertical,
                Distribution = UIStackViewDistribution.Fill,
                Alignment = UIStackViewAlignment.Fill,
                Spacing = 10f
            };

            _scrollView.AddSubview(_stackView);

            // 4. constrain all 4 sides to scrollView.contentLayoutGuide (10-pts padding)
            _stackView.TopAnchor.ConstraintEqualTo(_scrollView.ContentLayoutGuide.TopAnchor, 10f).Active = true;
            _stackView.LeadingAnchor.ConstraintEqualTo(_scrollView.ContentLayoutGuide.LeadingAnchor, 10f).Active = true;
            _stackView.TrailingAnchor.ConstraintEqualTo(_scrollView.ContentLayoutGuide.TrailingAnchor, -10f).Active = true;
            _stackView.BottomAnchor.ConstraintEqualTo(_scrollView.ContentLayoutGuide.BottomAnchor, -10f).Active = true;

            // 5. constrain stack view width to scrollView.frameLayoutGuide.widthAnchor -20 (10-pts on each side
            _stackView.WidthAnchor.ConstraintEqualTo(_scrollView.FrameLayoutGuide.WidthAnchor, 1, -20f).Active = true;

            // NO Height constraint for stackView

            
            // 6. create CardViews and add as arranged subviews of stackView
            qolCard = new CardView();
            _stackView.AddArrangedSubview(qolCard);
            qolCard.TranslatesAutoresizingMaskIntoConstraints = false;
            //qolCard.CornerRadius = 5f;
            //qolCard.ShadowOffsetHeight = 0;
            //qolCard.ShadowOffsetWidth = 0;
            qolCard.BackgroundColor = UIColor.White;

            goalsCard = new CardView();
            _stackView.AddArrangedSubview(goalsCard);
            goalsCard.TranslatesAutoresizingMaskIntoConstraints = false;
            //goalsCard.WidthAnchor.ConstraintEqualTo(_scrollView.FrameLayoutGuide.WidthAnchor, 1, -20).Active = true;
            //goalsCard.CornerRadius = 5f;
            //goalsCard.ShadowOffsetHeight = 0;
            //goalsCard.ShadowOffsetWidth = 0;
            goalsCard.BackgroundColor = UIColor.Green;


            // set scroll view background to red so we can see it
            _scrollView.BackgroundColor = UIColor.Red;

            updateCards();
        }

        public void RefreshControl_ValueChanged(object sender, EventArgs e)
        {
            Console.WriteLine("refreseh");
            updateCards();
            _scrollView.RefreshControl.EndRefreshing();
        }

        public void updateCards()
        {
            int n = iCardCounter % (words.GetUpperBound(0) + 1);
            qolCard.fillCard("Card: " + (n + 1) + " of " + (words.GetUpperBound(0) + 1) + "\n\n" + words[n, 0]);
            goalsCard.fillCard(words[n, 1]);
            iCardCounter += 1;
            
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        string[,] words = new string[,]
{

{
@"Label

A label can contain an arbitrary amount of text, but UILabel may shrink, wrap, or truncate the text, depending on the size of the bounding rectangle and properties you set. You can control the font, text color, alignment, highlighting, and shadowing of the text in the label.


Button

You can set the title, image, and other appearance properties of a button. In addition, you can specify a different appearance for each button state.",

@"Segmented Control

The segments can represent single or multiple selection, or a list of commands.

Each segment can display text or an image, but not both.


Text Field

Displays a rounded rectangle that can contain editable text. When a user taps a text field, a keyboard appears; when a user taps Return in the keyboard, the keyboard disappears and the text field can handle the input in an application-specific way.UITextField supports overlay views to display additional information, such as a bookmarks icon.UITextField also provides a clear text control a user taps to erase the contents of the text field."
},


{
@"Slider

UISlider displays a horizontal bar, called a track, that represents a range of values.The current value is shown by the position of an indicator, or thumb.A user selects a value by sliding the thumb along the track.You can customize the appearance of both the track and the thumb.


Switch

Displays an element that shows the user the boolean state of a given value.By tapping the control, the state can be toggled.",

@"Activity Indicator View

Used to indicate processing for a task with unknown completion percentage.


Progress View

Shows that a lengthy task is underway, and indicates the percentage of the task that has been completed.",
},


{
@"Page Control

UIPageControl indicates the number of open pages in an application by displaying a dot for each open page.The dot that corresponds to the currently viewed page is highlighted.UIPageControl supports navigation by sending the delegate an event when a user taps to the right or to the left of the currently highlighted dot.


Stepper

Often combined with a label or text field to show the value being incremented.",

@"Horizontal Stack View

An UIStackView creates and manages the constraints necessary to create horizontal or vertical stacks of views.It will dynamically add and remove its constraints to react to views being removed or added to its stack.With customization it can also react and influence the layout around it.


Vertical Stack View

An UIStackView creates and manages the constraints necessary to create horizontal or vertical stacks of views.It will dynamically add and remove its constraints to react to views being removed or added to its stack.With customization it can also react and influence the layout around it.",
},

{
@"Table View

Coordinates with a data source and delegate to display a scrollable list of rows.Each row in a table view is a UITableViewCell object.

The rows can be grouped into sections, and the sections can optionally have headers and footers.

The user can edit a table by inserting, deleting, and reordering table cells.


Table View Cell

Defines the attributes and behavior of cells in a table view.You can set a table cell's selected-state appearance, support editing functionality, display accessory views (such as a switch control), and specify background appearance and content indentation.",

@"Image View

Shows an image, or series of images as an animation.


Collection View

Coordinates with a data source and delegate to display a scrollable collection of cells.Each cell in a collection view is a UICollectionViewCell object.

Collection views support flow layout as well a custom layouts, and cells can be grouped into sections, and the sections and cells can optionally have supplementary views.",
},

};


    }
}