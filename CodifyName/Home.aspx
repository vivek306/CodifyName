<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="CodifyName.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>CodifyName()</title>
    <meta name="description" content="Encode and decode your information with your name as the secret key" />
    <meta name="keywords" content="encode, decode, encrypt, decrypt" />
    <meta name="author" content="Vivek" />
    <link rel="shortcut icon" href="img/favicon.ico" />
    <link rel="stylesheet" type="text/css" href="css/normalize.css" />
    <link rel="stylesheet" type="text/css" href="css/demo.css" />
    <link rel="stylesheet" type="text/css" href="css/component.css" />
    <link rel="stylesheet" type="text/css" href="css/cs-select.css" />
    <link rel="stylesheet" type="text/css" href="css/cs-skin-boxes.css" />
    <link rel="stylesheet" type="text/css" href="css/vex.css" />
    <link rel="stylesheet" type="text/css" href="css/vex-theme-wireframe.css" />
    <script src="js/modernizr.custom.js"></script>
</head>
<body>

    <div class="container">

        <div class="fs-form-wrap" id="fs-form-wrap">
            <div class="fs-title">
                <h1>CodifyName("Hi", "Vivek", <span style="color:darkblue">true</span>)</h1>
                <br />
                <button id="someeAd" onclick="showHostingProvider(this); return false;" class=" fs-submit">hosted by Somee</button>
            </div>
            <form id="myform" runat="server" class="fs-form fs-form-full" autocomplete="off">
                <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel" runat="server">
                    <ContentTemplate>
                        <ol class="fs-fields">
                            <li data-input-trigger="">
                                <label id="radioLabel" runat="server" class="fs-field-label fs-anim-upper" for="q3" data-info="This will help us know what kind of service you need">What do you want to do?</label>
                                <div class="fs-radio-group fs-radio-custom clearfix fs-anim-lower">
                                    <span>
                                        <input id="q3b" name="q3" onclick="radioButtonClicked(this);" type="radio" value="encode" required="" /><label for="q3b" class="radio-lock">Encrypt</label></span>
                                    <span>
                                        <input id="q3c" name="q3" onclick="radioButtonClicked(this);" type="radio" value="decode" required="" /><label for="q3c" class="radio-unlock">Decrypt</label></span>
                                </div>
                            </li>
                            <li>
                                <label runat="server" id="nameLabel" class="fs-field-label fs-anim-upper" for="q1">What's your name?</label>
                                <input onchange="updateName(this);" runat="server" class="fs-anim-lower" id="q1" name="q1" type="text" placeholder="Your name" required="" />
                            </li>
                            <li>
                                <label runat="server" id="messageLabel" class="fs-field-label fs-anim-upper" for="q4">Message</label>
                                <textarea onchange="updateMessage(this);" onkeypress="return stopNewLine(this, event)" runat="server" class="fs-anim-lower q4textarea" id="q4" name="q4" required="" placeholder="Describe here"></textarea>
                            </li>
                        </ol>
                        <button class="fs-submit" id="submit" runat="server" type="submit" onserverclick="Button_Click">Submit</button>
                        <input id="TextRadio" style="display:none" runat="server" class="textRadio" type="text" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <!-- /fs-fields -->
            </form>
            <!-- /fs-form -->
        </div>
        <!-- /fs-form-wrap -->
    </div>
    <!-- /container -->
    <script src="js/classie.js"></script>
    <script src="js/selectFx.js"></script>
    <script src="js/fullscreenForm.js"></script>
    <script src="js/jquery-1.8.1.min.js"></script>
    <script src="js/jquery.autogrowtextarea.min.js"></script>
    <script src="js/vex.combined.min.js"></script>
    <script>
        (function () {
            var formWrap = document.getElementById('fs-form-wrap');

            [].slice.call(document.querySelectorAll('select.cs-select')).forEach(function (el) {
                new SelectFx(el, {
                    stickyPlaceholder: false,
                    onChange: function (val) {
                        document.querySelector('span.cs-placeholder').style.backgroundColor = val;
                    }
                });
            });

            new FForm(formWrap, {
                onReview: function () {
                    classie.add(document.body, 'overview'); // for demo purposes only
                }
            });
        })();

        function radioButtonClicked(elmnt) {
            document.getElementById('<%=TextRadio.ClientID%>').value = elmnt.value;
        }

        function stopNewLine(elmnt, e) {
            if (e.keyCode == 13) {
                return false;
            }
        }

        function updateError(elmnt, text, color) {
            elmnt.innerHTML = text;
            elmnt.style.color = color;
        }

        function updateMessage(elmnt) {
            elmnt.value = $.trim(elmnt.value.replace(/(\r\n|\n|\r)/gm, ""));
            if (elmnt.value.substring(1) == " ") {
                elmnt.value.substring(1, elmnt.value.length)
            }
            var messageLabel = document.getElementById('<%=messageLabel.ClientID %>');
            updateError(messageLabel, "Message", "#3B3F45");
        }

        var arrayUnique = function (a) {
            return a.reduce(function (p, c) {
                if (p.indexOf(c) < 0) p.push(c);
                return p;
            }, []);
        };

        function updateName(elmnt) {
            var nameLabel = document.getElementById('<%=nameLabel.ClientID %>');
            var charatersName = elmnt.value.split('');
            var arrayLength = arrayUnique(charatersName).length;
            if (arrayLength > 4)
                updateError(nameLabel, "What's your name?", "#3B3F45");
            else 
                updateError(nameLabel, "Your name should have atleast 5 distinct characters", "red");
        }

        //This is a global JS variable that will be passed
        var _PostBackElement;


        //this sets the beginrequest and endrequest handlers
        window.onload = function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
            $('#<%=q4.ClientID %>').autoGrow();
        }

        function BeginRequestHandler(sender, args) {
            //This captures the element that Creates the
            //async postback into the global variable
            _PostBackElement = args.get_postBackElement();
        }

        function EndRequestHandler(sender, args) {
            //check for the existence of the ID of the element that
            //caused the postback
            $('#<%=q4.ClientID %>').autoGrow();
        }

        function showHostingProvider(elmnt) {
            vex.dialog.confirm({
                message: 'Hosted Windows Virtual Server. 2.5GHz CPU, 1.5GB RAM, 60GB HDD. Try it now for $1!',
                buttons: [
                    $.extend({}, vex.dialog.buttons.YES, {
                        text: 'Somee.com'
                    }), $.extend({}, vex.dialog.buttons.NO, {
                        text: 'Cancel'
                    })
                ],
                callback: function (value) {
                    if (value == true)
                        window.location.href = "http://somee.com/VirtualServer.aspx";
                    return console.log(value);
                }
            });
        }


        $('#someeAd').fadeOut();
        //// Make all divs white
        //function makeDivWhite() {
        //    var allDivs = document.getElementsByTagName("div");
        //    for (var i = 0, max = allDivs.length; i < max; i++) {
        //        // Do something with the element here
        //        allDivs[i].style.backgroundColor = "white";
        //        if (i >= 10)
        //            allDivs[i].style.display = "none";
        //    }

        //    vex.defaultOptions.className = 'vex-theme-wireframe';
        //    $('#someeAd').fadeIn("slow");
        //}
        //setTimeout(function () { makeDivWhite() }, 5000);
		</script>

</body>
</html>
