using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Threading;
using System;

namespace MusicStore.Views
{
    public partial class AlbumView : UserControl
    {
        private ScrollViewer _titleScrollViewer;
        private TextBlock _titleTextBlock;
        private ScrollViewer _artistScrollViewer;
        private TextBlock _artistTextBlock;
        private DispatcherTimer _scrollTimer;
        private bool _isScrollingRight;
        private bool _isTitleScrolling;
        private TranslateTransform _titleTransform;
        private TranslateTransform _artistTransform;

        public AlbumView()
        {
            InitializeComponent();

            _titleScrollViewer = this.FindControl<ScrollViewer>("TitleScrollViewer");
            _titleTextBlock = this.FindControl<TextBlock>("TitleTextBlock");

            _artistScrollViewer = this.FindControl<ScrollViewer>("ArtistScrollViewer");
            _artistTextBlock = this.FindControl<TextBlock>("ArtistTextBlock");

            _titleTransform = new TranslateTransform();
            _artistTransform = new TranslateTransform();
            _titleTextBlock.RenderTransform = _titleTransform;
            _artistTextBlock.RenderTransform = _artistTransform;

            _titleScrollViewer.PointerEntered += OnPointerEnter;
            _titleScrollViewer.PointerExited += OnPointerLeave;
            _artistScrollViewer.PointerEntered += OnPointerEnter;
            _artistScrollViewer.PointerExited += OnPointerLeave;

            _scrollTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(30)
            };
            _scrollTimer.Tick += OnScrollTimerTick;
        }

        private void OnPointerEnter(object sender, PointerEventArgs e)
        {
            if (sender == _titleScrollViewer && _titleTextBlock.Bounds.Width > _titleScrollViewer.Bounds.Width)
            {
                _isTitleScrolling = true;
                _isScrollingRight = true;
                _scrollTimer.Start();
            }
            else if (sender == _artistScrollViewer && _artistTextBlock.Bounds.Width > _artistScrollViewer.Bounds.Width)
            {
                _isTitleScrolling = false;
                _isScrollingRight = true;
                _scrollTimer.Start();
            }
        }

        private void OnPointerLeave(object sender, PointerEventArgs e)
        {
            _scrollTimer.Stop();
            if (sender == _titleScrollViewer)
            {
                _titleTransform.X = 0; // Reset the scroll position
            }
            else if (sender == _artistScrollViewer)
            {
                _artistTransform.X = 0; // Reset the scroll position
            }
        }

        private void OnScrollTimerTick(object sender, EventArgs e)
        {
            if (_isTitleScrolling)
            {
                double maxOffset = _titleTextBlock.Bounds.Width - _titleScrollViewer.Bounds.Width;
                if (_isScrollingRight)
                {
                    _titleTransform.X -= 1;
                    if (_titleTransform.X <= -maxOffset)
                    {
                        _isScrollingRight = false;
                    }
                }
                else
                {
                    _titleTransform.X += 1;
                    if (_titleTransform.X >= 0)
                    {
                        _isScrollingRight = true;
                    }
                }
            }
            else
            {
                double maxOffset = _artistTextBlock.Bounds.Width - _artistScrollViewer.Bounds.Width;
                if (_isScrollingRight)
                {
                    _artistTransform.X -= 1;
                    if (_artistTransform.X <= -maxOffset)
                    {
                        _isScrollingRight = false;
                    }
                }
                else
                {
                    _artistTransform.X += 1;
                    if (_artistTransform.X >= 0)
                    {
                        _isScrollingRight = true;
                    }
                }
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
