using WeifenLuo.WinFormsUI.Docking;

namespace MAIDE.UI
{
    public class DockingTheme : ThemeBase
    {
        public DockingTheme()
        {
            Skin = new DockPanelSkin();
            Palette.PaletteChanged += Palette_PropertyChanged;
            Palette_PropertyChanged(null, null);
        }

        private void Palette_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Skin.AutoHideStripSkin.DockStripGradient.StartColor = Palette.GetColor("Background");
            Skin.AutoHideStripSkin.DockStripGradient.EndColor = Palette.GetColor("Background");

            Skin.AutoHideStripSkin.TabGradient.StartColor = Palette.GetColor("DockingTabDisable");
            Skin.AutoHideStripSkin.TabGradient.EndColor = Palette.GetColor("DockingTabDisable");
            Skin.AutoHideStripSkin.TabGradient.TextColor = Palette.GetColor("FontMain");

            Skin.DockPaneStripSkin.DocumentGradient.DockStripGradient.StartColor = Palette.GetColor("Background");
            Skin.DockPaneStripSkin.DocumentGradient.DockStripGradient.EndColor = Palette.GetColor("Background");
            Skin.DockPaneStripSkin.ToolWindowGradient.DockStripGradient.StartColor = Palette.GetColor("Background");
            Skin.DockPaneStripSkin.ToolWindowGradient.DockStripGradient.EndColor = Palette.GetColor("Background");

            Skin.DockPaneStripSkin.DocumentGradient.ActiveTabGradient.StartColor = Palette.GetColor("DockingTabActive");
            Skin.DockPaneStripSkin.DocumentGradient.ActiveTabGradient.EndColor = Palette.GetColor("DockingTabActive");
            Skin.DockPaneStripSkin.DocumentGradient.ActiveTabGradient.TextColor = Palette.GetColor("FontMain");
            Skin.DockPaneStripSkin.ToolWindowGradient.ActiveTabGradient.StartColor = Palette.GetColor("DockingTabActive");
            Skin.DockPaneStripSkin.ToolWindowGradient.ActiveTabGradient.EndColor = Palette.GetColor("DockingTabActive");
            Skin.DockPaneStripSkin.ToolWindowGradient.ActiveTabGradient.TextColor = Palette.GetColor("FontMain");

            Skin.DockPaneStripSkin.DocumentGradient.InactiveTabGradient.StartColor = Palette.GetColor("DockingTabDisable");
            Skin.DockPaneStripSkin.DocumentGradient.InactiveTabGradient.EndColor = Palette.GetColor("DockingTabDisable");
            Skin.DockPaneStripSkin.DocumentGradient.InactiveTabGradient.TextColor = Palette.GetColor("FontMain");
            Skin.DockPaneStripSkin.ToolWindowGradient.InactiveTabGradient.StartColor = Palette.GetColor("DockingTabDisable");
            Skin.DockPaneStripSkin.ToolWindowGradient.InactiveTabGradient.EndColor = Palette.GetColor("DockingTabDisable");
            Skin.DockPaneStripSkin.ToolWindowGradient.InactiveTabGradient.TextColor = Palette.GetColor("FontMain");

            Skin.DockPaneStripSkin.ToolWindowGradient.ActiveCaptionGradient.StartColor = Palette.GetColor("DockingTabActive");
            Skin.DockPaneStripSkin.ToolWindowGradient.ActiveCaptionGradient.EndColor = Palette.GetColor("DockingTabActive");
            Skin.DockPaneStripSkin.ToolWindowGradient.ActiveCaptionGradient.TextColor = Palette.GetColor("FontMain");

            Skin.DockPaneStripSkin.ToolWindowGradient.InactiveCaptionGradient.StartColor = Palette.GetColor("DockingTabDisable");
            Skin.DockPaneStripSkin.ToolWindowGradient.InactiveCaptionGradient.EndColor = Palette.GetColor("DockingTabDisable");
            Skin.DockPaneStripSkin.ToolWindowGradient.InactiveCaptionGradient.TextColor = Palette.GetColor("FontMain");
        }

        public override void Apply(DockPanel dockPanel)
        {
        }
    }
}