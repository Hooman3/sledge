﻿using System.Collections.Generic;
using System.Drawing;
using Sledge.EditorNew.Properties;
using Sledge.Settings;

namespace Sledge.EditorNew.Tools
{
    public class DummyTool : BaseTool
    {
        public override IEnumerable<string> GetContexts()
        {
            yield return "DummyTool";
        }

        public override Image GetIcon()
        {
            return Resources.Tool_Select;
        }

        public override string GetName()
        {
            return "Dummy";
        }

        public override HotkeyTool? GetHotkeyToolType()
        {
            return null;
        }

        public override HotkeyInterceptResult InterceptHotkey(HotkeysMediator hotkeyMessage, object parameters)
        {
            return HotkeyInterceptResult.Continue;
        }
    }
}
