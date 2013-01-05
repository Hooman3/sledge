﻿using System;
using System.Linq;
using System.Windows.Forms;
using Sledge.Editor.Documents;

namespace Sledge.Editor.Visgroups
{
    public static class VisgroupManager
    {
        private static VisgroupPanel _visgroupPanel;
        private static Document _currentDocument;

        public static void SetCurrentDocument(Document doc)
        {
            _currentDocument = doc;
            Update();
        }

        public static void SetVisgroupPanel(Control panel)
        {
            _visgroupPanel = panel.Controls.OfType<VisgroupPanel>().First();
            _visgroupPanel.VisgroupToggled += VisgroupToggled;

            var editButton = panel.Controls.OfType<Button>().First(x => x.Tag is string && (string)x.Tag == "Edit");
            editButton.Click += ShowEditDialog;
            var selectButton = panel.Controls.OfType<Button>().First(x => x.Tag is string && (string)x.Tag == "Select");
            selectButton.Click += SelectVisgroup;
            var showAllButton = panel.Controls.OfType<Button>().First(x => x.Tag is string && (string)x.Tag == "ShowAll");
            showAllButton.Click += ShowAllVisgroups;
        }

        private static void ShowEditDialog(object sender, EventArgs e)
        {
            if (_currentDocument == null) return;
            using (var vef = new VisgroupEditForm(_currentDocument))
            {
                vef.ShowDialog();
                Update();
                if (vef.NeedReload)
                {
                    _currentDocument.UpdateDisplayLists();
                }
            }
        }

        private static void SelectVisgroup(object sender, EventArgs e)
        {
            if (_currentDocument == null) return;
            throw new NotImplementedException();
        }

        private static void ShowAllVisgroups(object sender, EventArgs e)
        {
            if (_currentDocument == null) return;
            Update();
            Application.DoEvents();
            _currentDocument.Map.Visgroups.ForEach(x => x.Visible = true);
            _currentDocument.Map.WorldSpawn.ForEach(x => x.IsVisgroupHidden, x => x.IsVisgroupHidden = false);
            _currentDocument.UpdateDisplayLists();
        }

        private static void VisgroupToggled(object sender, int visgroupId, CheckState state)
        {
            if (_currentDocument == null) return;

            if (state == CheckState.Indeterminate) return;
            var visible = state == CheckState.Checked;
            // Hide all the objects in the visgroup
            var visItems = _currentDocument.Map.WorldSpawn.Find(x => x.IsInVisgroup(visgroupId), true);
            visItems.ForEach(x => x.IsVisgroupHidden = !visible);

            // Grey out the other visgroups those objects are in
            var otherGroups = visItems.SelectMany(x => x.Visgroups).Distinct().Where(x => x != visgroupId).ToList();
            foreach (var otherGroup in otherGroups)
            {
                var oid = otherGroup;
                if (visible)
                {
                    // Find items that are still invisible, if there are any then we set the state to indeterminate
                    var visibleInGroup = _currentDocument.Map.WorldSpawn.Find(x => x.IsInVisgroup(oid) && x.IsVisgroupHidden, true);
                    // The state cannot be unchecked because we have just shown one - if we have hidden items then indeterminate, else checked.
                    _visgroupPanel.SetCheckState(oid, visibleInGroup.Any() ? CheckState.Indeterminate : CheckState.Checked);
                }
                else
                {
                    // Get the ones that are still visible
                    var visibleInGroup = _currentDocument.Map.WorldSpawn.Find(x => x.IsInVisgroup(oid) && !x.IsVisgroupHidden, true);
                    _visgroupPanel.SetCheckState(oid, visibleInGroup.Any() ? CheckState.Indeterminate : CheckState.Unchecked);
                }
            }

            _currentDocument.UpdateDisplayLists();
        }

        public static void Update()
        {
            Clear();
            if (_visgroupPanel == null || _currentDocument == null) return;
            _visgroupPanel.Update(_currentDocument.Map.Visgroups);
        }

        public static void Clear()
        {
            if (_visgroupPanel == null) return;
            _visgroupPanel.Clear();
        }
    }
}
