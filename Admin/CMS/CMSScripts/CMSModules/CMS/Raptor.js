﻿cmsdefine(["CMS/Core", "jQuery"], function (e, t) { return Module = function (a) { var S = new e(a).ctx.data, d = t("#" + S.labelId), u = 0, R = !0; speak = function () { if ("speechSynthesis" in window) { var e = new SpeechSynthesisUtterance; e.text = "Raptor is here!", e.rate = .2, e.pitch = 0, e.voice = speechSynthesis.getVoices()[0], window.speechSynthesis.speak(e) } }; var n = t("<img style='position:fixed;bottom:0;right:0;display:none;' />"); n.bind("click", speak), n.attr("src", "data:image/png;base64,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"), d.parent().after(n), d.bind("click", function () { 12 == ++u && (R ? (n.fadeIn(), speak()) : n.fadeOut(), u = 0, R = !R) }) }, Module });