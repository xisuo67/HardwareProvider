using Animation;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareProvider
{
    public class YesNoAnimationCtrl : Control, IAnimationObserver
    {
        private const int CUSTOMSIZE = 100;

        private int m_minArcAngle = 45;
        private int m_maxArcAngle = 315;
        private int m_minAngel = 15;
        private int m_currentAngle;
        private int m_nextAngle;
        private int m_currentArc;
        private bool m_isCollapsed;
        private bool m_isSwitch;
        private YesNoWarn m_yesnowarn;
        private int m_currentScanLR;
        private int m_currentScanRL;
        private int m_currentScanTB;
        private IntLinearAnimation m_loadingAnimation;
        private IntLinearAnimation m_completeAnimation;
        private IntLinearAnimation m_yesAnimation;
        private IntLinearAnimation m_warnAnimation;
        private IntLinearAnimation m_no1Animation;
        private IntLinearAnimation m_no2Animation;

        public YesNoAnimationCtrl()
        {
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer, true);
            this.Size = new Size(CUSTOMSIZE, CUSTOMSIZE);
            this.Resize += YesNoAnimationCtrl_Resize;
            this.BackColorChanged += YesNoAnimationCtrl_BackColorChanged;

            this.m_yesAnimation = new IntLinearAnimation("Yes");
            this.m_yesAnimation.From = 0;
            this.m_yesAnimation.To = CUSTOMSIZE;
            this.m_yesAnimation.Duration = 500;
            this.m_yesAnimation.AddObserver(this);

            this.m_warnAnimation = new IntLinearAnimation("Warn");
            this.m_warnAnimation.From = 0;
            this.m_warnAnimation.To = CUSTOMSIZE;
            this.m_warnAnimation.Duration = 500;
            this.m_warnAnimation.AddObserver(this);

            this.m_no2Animation = new IntLinearAnimation("No2");
            this.m_no2Animation.From = 0;
            this.m_no2Animation.To = CUSTOMSIZE;
            this.m_no2Animation.Duration = 250;
            this.m_no2Animation.AddObserver(this);

            this.m_no1Animation = new IntLinearAnimation("No1");
            this.m_no1Animation.From = 0;
            this.m_no1Animation.To = CUSTOMSIZE;
            this.m_no1Animation.Duration = 250;
            this.m_no1Animation.AddObserver(this);
            this.m_no1Animation.NextAnimation = this.m_no2Animation;

            this.m_completeAnimation = new IntLinearAnimation("Complete");
            this.m_completeAnimation.From = 0;
            this.m_completeAnimation.To = 360;
            this.m_completeAnimation.Duration = 500;
            this.m_completeAnimation.AddObserver(this);

            this.m_loadingAnimation = new IntLinearAnimation("Loading");
            this.m_loadingAnimation.From = this.m_minArcAngle;
            this.m_loadingAnimation.To = this.m_maxArcAngle;
            this.m_loadingAnimation.Step = this.m_minAngel;
            this.m_loadingAnimation.RepeatBehavior = RepeatBehavior.Back;
            this.m_loadingAnimation.RepeatCount = -1;
            this.m_loadingAnimation.AddObserver(this);
            this.m_loadingAnimation.NextAnimation = this.m_completeAnimation;
        }

        private void YesNoAnimationCtrl_BackColorChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void YesNoAnimationCtrl_Resize(object sender, EventArgs e)
        {
            this.Height = this.Width;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            SolidBrush bgBrush = new SolidBrush(this.BackColor);
            graphics.FillRectangle(bgBrush, new Rectangle(new Point(0, 0), this.Size));
            bgBrush.Dispose();

            float zoom = (float)this.Width / CUSTOMSIZE;
            graphics.MultiplyTransform(new Matrix(zoom, 0, 0, zoom, 0, 0));
            graphics.MultiplyTransform(new Matrix(1, 0, 0, 1, CUSTOMSIZE / 2F, CUSTOMSIZE / 2F));
            Pen arcPen = new Pen(Color.FromArgb(59, 91, 179), 4F / zoom);
            RectangleF rect = new RectangleF(
                new PointF(-CUSTOMSIZE / 2F + 4F / zoom, -CUSTOMSIZE / 2F + 4F / zoom),
                new SizeF(CUSTOMSIZE - 8F / zoom, CUSTOMSIZE - 8F / zoom));
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            if (this.m_loadingAnimation.IsWorking)
            {
                if (!this.m_isCollapsed)
                {
                    graphics.DrawArc(arcPen, rect, this.m_currentAngle, this.m_currentArc);
                    if (this.m_isSwitch)
                    {
                        this.m_isSwitch = false;
                        this.m_isCollapsed = true;
                    }
                }
                else
                {
                    graphics.DrawArc(arcPen, rect, this.m_currentAngle - this.m_currentArc, this.m_currentArc);
                    if (this.m_isSwitch)
                    {
                        this.m_isSwitch = false;
                        this.m_isCollapsed = false;
                    }
                }
            }
            else
            {
                Color arcPenColor = Color.FromArgb(79, 156, 77);
                if (this.m_yesnowarn == YesNoWarn.No)
                {
                    arcPenColor = Color.FromArgb(203, 57, 58);
                }
                else if (this.m_yesnowarn == YesNoWarn.Warn)
                {
                    arcPenColor = Color.FromArgb(253, 184, 47);
                }
                arcPen.Color = arcPenColor;
                graphics.DrawArc(arcPen, rect, 0, this.m_currentArc);

                Pen yesnowarnPen = new Pen(arcPenColor, 4F / zoom);
                if (this.m_yesnowarn == YesNoWarn.Yes)
                {
                    graphics.SetClip(new RectangleF(new PointF(-CUSTOMSIZE / 2F, -CUSTOMSIZE / 2F),
                        new SizeF(this.m_currentScanLR, CUSTOMSIZE)));
                    PointF[] points = new PointF[]
                    {
                    new PointF(-20, 0),
                    new PointF(-5, 15),
                    new PointF(20, -15)
                    };
                    graphics.DrawLines(yesnowarnPen, points);
                    graphics.ResetClip();
                }
                else if (this.m_yesnowarn == YesNoWarn.Warn)
                {
                    graphics.SetClip(new RectangleF(new PointF(-CUSTOMSIZE / 2F, -CUSTOMSIZE / 2F),
                        new SizeF(CUSTOMSIZE, this.m_currentScanTB)));
                    graphics.DrawLine(yesnowarnPen, new PointF(0, -20), new PointF(0, 10));
                    graphics.DrawEllipse(yesnowarnPen, new RectangleF(new PointF(-2, 20), new SizeF(4, 4)));
                    graphics.ResetClip();
                }
                else if (this.m_yesnowarn == YesNoWarn.No)
                {
                    graphics.SetClip(new RectangleF(new PointF(-CUSTOMSIZE / 2F, -CUSTOMSIZE / 2F),
                        new SizeF(this.m_currentScanLR, CUSTOMSIZE)));
                    graphics.DrawLine(yesnowarnPen, new PointF(-20, -20), new PointF(20, 20));
                    graphics.ResetClip();

                    graphics.SetClip(new RectangleF(new PointF(CUSTOMSIZE / 2F - this.m_currentScanRL, -CUSTOMSIZE / 2F),
                        new SizeF(this.m_currentScanRL, CUSTOMSIZE)));
                    graphics.DrawLine(yesnowarnPen, new PointF(20, -20), new PointF(-20, 20));
                    graphics.ResetClip();
                }
                yesnowarnPen.Dispose();
            }
            arcPen.Dispose();
            graphics.SmoothingMode = SmoothingMode.Default;
            graphics.ResetTransform();
        }

        public void AnimationUpdated(IAnimation animation)
        {
            this.BeginInvoke(new Action(
                () =>
                {
                    if (animation.Name == "Loading")
                    {
                        this.m_currentArc = (animation as IntLinearAnimation).Current;
                        this.m_currentAngle = this.m_nextAngle;
                        this.Invalidate();
                        if (this.m_isCollapsed != animation.IsBack)
                        {
                            this.m_isSwitch = true;
                            if (this.m_isCollapsed)
                            {
                                this.m_nextAngle = this.m_currentAngle - this.m_currentArc;
                            }
                            else
                            {
                                this.m_nextAngle = this.m_currentAngle + this.m_currentArc;
                            }
                        }
                        if (this.m_nextAngle > 360)
                        {
                            this.m_nextAngle = this.m_nextAngle % 360;
                        }
                        this.m_nextAngle += this.m_minAngel;
                    }
                    else if (animation.Name == "Complete")
                    {
                        this.m_currentArc = (animation as IntLinearAnimation).Current;
                        this.Invalidate();
                    }
                    else if (animation.Name == "Yes" || animation.Name == "No1")
                    {
                        this.m_currentScanLR = (animation as IntLinearAnimation).Current;
                        this.Invalidate();
                    }
                    else if (animation.Name == "Warn")
                    {
                        this.m_currentScanTB = (animation as IntLinearAnimation).Current;
                        this.Invalidate();
                    }
                    else if (animation.Name == "No2")
                    {
                        this.m_currentScanRL = (animation as IntLinearAnimation).Current;
                        this.Invalidate();
                    }
                }));
        }

        public void Start()
        {
            this.m_currentAngle = 0;
            this.m_nextAngle = 0;
            this.m_currentArc = 0;
            this.m_isCollapsed = false;
            this.m_loadingAnimation.Start();
        }

        public void Stop(YesNoWarn yesnowarn)
        {
            this.m_currentScanLR = 0;
            this.m_currentScanRL = 0;
            this.m_currentScanTB = 0;
            this.m_yesnowarn = yesnowarn;
            if (this.m_yesnowarn != YesNoWarn.Unknown)
            {
                this.m_completeAnimation.NextAnimation = this.m_yesAnimation;
                if (yesnowarn == YesNoWarn.Warn)
                {
                    this.m_completeAnimation.NextAnimation = this.m_warnAnimation;
                }
                else if (yesnowarn == YesNoWarn.No)
                {
                    this.m_completeAnimation.NextAnimation = this.m_no1Animation;
                }
            }
            this.m_loadingAnimation.Stop();
        }
    }

    public enum YesNoWarn
    {
        Unknown,
        Yes,
        No,
        Warn
    }
}
