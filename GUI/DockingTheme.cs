using WeifenLuo.WinFormsUI.Docking;

namespace MAIDE.UI
{
    public class DockingTheme : ThemeBase
    {
        public DockingTheme()
        {
            Skin = new DockPanelSkin();
            Palette.PropertyChanged += Palette_PropertyChanged;
            Palette_PropertyChanged(null, null);
        }

        private void Palette_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Skin.AutoHideStripSkin.DockStripGradient.StartColor = Palette.Background;
            Skin.AutoHideStripSkin.DockStripGradient.EndColor = Palette.Background;

            Skin.AutoHideStripSkin.TabGradient.StartColor = Palette.DockingTabDisable;
            Skin.AutoHideStripSkin.TabGradient.EndColor = Palette.DockingTabDisable;
            Skin.AutoHideStripSkin.TabGradient.TextColor = Palette.FontMain;

            Skin.DockPaneStripSkin.DocumentGradient.DockStripGradient.StartColor = Palette.Background;
            Skin.DockPaneStripSkin.DocumentGradient.DockStripGradient.EndColor = Palette.Background;
            Skin.DockPaneStripSkin.ToolWindowGradient.DockStripGradient.StartColor = Palette.Background;
            Skin.DockPaneStripSkin.ToolWindowGradient.DockStripGradient.EndColor = Palette.Background;

            Skin.DockPaneStripSkin.DocumentGradient.ActiveTabGradient.StartColor = Palette.DockingTabActive;
            Skin.DockPaneStripSkin.DocumentGradient.ActiveTabGradient.EndColor = Palette.DockingTabActive;
            Skin.DockPaneStripSkin.DocumentGradient.ActiveTabGradient.TextColor = Palette.FontMain;
            Skin.DockPaneStripSkin.ToolWindowGradient.ActiveTabGradient.StartColor = Palette.DockingTabActive;
            Skin.DockPaneStripSkin.ToolWindowGradient.ActiveTabGradient.EndColor = Palette.DockingTabActive;
            Skin.DockPaneStripSkin.ToolWindowGradient.ActiveTabGradient.TextColor = Palette.FontMain;

            Skin.DockPaneStripSkin.DocumentGradient.InactiveTabGradient.StartColor = Palette.DockingTabDisable;
            Skin.DockPaneStripSkin.DocumentGradient.InactiveTabGradient.EndColor = Palette.DockingTabDisable;
            Skin.DockPaneStripSkin.DocumentGradient.InactiveTabGradient.TextColor = Palette.FontMain;
            Skin.DockPaneStripSkin.ToolWindowGradient.InactiveTabGradient.StartColor = Palette.DockingTabDisable;
            Skin.DockPaneStripSkin.ToolWindowGradient.InactiveTabGradient.EndColor = Palette.DockingTabDisable;
            Skin.DockPaneStripSkin.ToolWindowGradient.InactiveTabGradient.TextColor = Palette.FontMain;
            
            Skin.DockPaneStripSkin.ToolWindowGradient.ActiveCaptionGradient.StartColor = Palette.DockingTabActive;
            Skin.DockPaneStripSkin.ToolWindowGradient.ActiveCaptionGradient.EndColor = Palette.DockingTabActive;
            Skin.DockPaneStripSkin.ToolWindowGradient.ActiveCaptionGradient.TextColor = Palette.FontMain;

            Skin.DockPaneStripSkin.ToolWindowGradient.InactiveCaptionGradient.StartColor = Palette.DockingTabDisable;
            Skin.DockPaneStripSkin.ToolWindowGradient.InactiveCaptionGradient.EndColor = Palette.DockingTabDisable;
            Skin.DockPaneStripSkin.ToolWindowGradient.InactiveCaptionGradient.TextColor = Palette.FontMain;
        }

        public override void Apply(DockPanel dockPanel)
        {
        }
    }
}