
$(document).ready(function () {
    var xhr = new XMLHttpRequest();
    xhr.open("GET", "/Search/FindAllFucNames");
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            Autosuggest.Initialize(JSON.parse(xhr.responseText));
        }
    };
    xhr.send(null);
   
});

Autosuggest =
{
    Initialize: function (fuclist) {
        var suggestionListObj = {
            'data': fuclist,
            'isVisible': false,
            'element': document.getElementById('searchQuery'),
            'dropdown': null,
            'highlighted': null
        };

        suggestionListObj['element'].setAttribute('autocomplete', 'off'); //inibir atributo autocomplete de tag input
        suggestionListObj['element'].onkeydown = function (e) { return Autosuggest.KeyDown(suggestionListObj, e); };
        suggestionListObj['element'].onkeyup = function (e) { return Autosuggest.KeyUp(suggestionListObj, e); };
        suggestionListObj['element'].onkeypress = function (e) {
            if (!e) e = window.event;
            if (e.keyCode == 13) return false;
        };
        suggestionListObj['element'].ondblclick = function () { Autosuggest.ShowDropdown(suggestionListObj); };
        suggestionListObj['element'].onclick = function (e) {
            if (!e) e = window.event;
            e.cancelBubble = true;
            e.returnValue = false;
        };

        //esconder o menu de dropdown quando página clicada
        var docClick = function () {
            Autosuggest.HideDropdown(suggestionListObj);
        };

        if (document.addEventListener) {
            document.addEventListener('click', docClick, false);
        } else if (document.attachEvent) {
            document.attachEvent('onclick', docClick, false);
        }


        Autosuggest.CreateDropdown(suggestionListObj);

    },


    CreateDropdown: function (suggestionListObj) {
        var left = this.GetLeft(suggestionListObj['element']);
        var top = this.GetTop(suggestionListObj['element']) + suggestionListObj['element'].offsetHeight;
        var width = suggestionListObj['element'].offsetWidth;

        suggestionListObj['dropdown'] = document.createElement('div');
        suggestionListObj['dropdown'].className = 'autocomplete'; // Don't use setAttribute()

        suggestionListObj['element'].parentNode.insertBefore(suggestionListObj['dropdown'], suggestionListObj['element']);

        //alterar posição de menu dropdown com valores obtidos
        suggestionListObj['dropdown'].style.left = left + 'px';
        suggestionListObj['dropdown'].style.top = top + 'px';
        suggestionListObj['dropdown'].style.width = width + 'px';
        suggestionListObj['dropdown'].style.zIndex = '99';
        suggestionListObj['dropdown'].style.visibility = 'hidden';
    },


    
    GetLeft: function(elemRef) {
    var curNode = elemRef;
    var left = 0;

    do {
    left += curNode.offsetLeft;
    curNode = curNode.offsetParent;

    } while (curNode.tagName.toLowerCase() != 'body');

    return left;
    },


    GetTop: function (elemRef) {
        var curNode = elemRef;
        var top = 0;
        do {
            top += curNode.offsetTop;
            curNode = curNode.offsetParent;

        } while (curNode.tagName.toLowerCase() != 'body');

        return top;
    },


    ShowDropdown: function (suggestionListObj) {
        this.HideDropdown(suggestionListObj);

        var value = suggestionListObj['element'].value;
        var toDisplay = new Array();
        var newDiv = null;
        var text = null;
        var numItems = suggestionListObj['dropdown'].childNodes.length;

        // Remove all child nodes from dropdown
        while (suggestionListObj['dropdown'].childNodes.length > 0) {
            suggestionListObj['dropdown'].removeChild(suggestionListObj['dropdown'].childNodes[0]);
        }

        // Go thru data searching for matches
        for (i = 0; i < suggestionListObj['data'].length; ++i) {
            if (suggestionListObj['data'][i].toLowerCase().indexOf(value) >= 0) {
                toDisplay[toDisplay.length] = suggestionListObj['data'][i];
            }
        }

        // No matches?
        if (toDisplay.length == 0) {
            this.HideDropdown(suggestionListObj);
            return;
        }


        // Add data to the dropdown layer
        for (i = 0; i < toDisplay.length; ++i) {
            newDiv = document.createElement('div');
            newDiv.className = 'autocomplete_item'; // Don't use setAttribute()
            newDiv.setAttribute('id', 'autocomplete_item_' + i);
            newDiv.setAttribute('index', i);
            newDiv.style.zIndex = '99';

            // Scrollbars are on display ?
            if (toDisplay.length > suggestionListObj['maxitems'] && navigator.userAgent.indexOf('MSIE') == -1) {
                newDiv.style.width = suggestionListObj['element'].offsetWidth - 22 + 'px';
            }

            newDiv.onmouseover = function () {
                 Autosuggest.HighlightItem(suggestionListObj, this.getAttribute('index'));
            };
            newDiv.onclick = function () {
                Autosuggest.SetValue(suggestionListObj);
                Autosuggest.HideDropdown(suggestionListObj);
            };

            text = document.createTextNode(toDisplay[i]);
            newDiv.appendChild(text);

            suggestionListObj['dropdown'].appendChild(newDiv);
        }


        // Too many items?
        if (toDisplay.length > suggestionListObj['maxitems']) {
            suggestionListObj['dropdown'].style.height = (suggestionListObj['maxitems'] * 15) + 2 + 'px';

        } else {
            suggestionListObj['dropdown'].style.height = '';
        }


        /**
        * Set left/top in case of document movement/scroll/window resize etc
        */
        suggestionListObj['dropdown'].style.left = this.GetLeft(suggestionListObj['element']);
        suggestionListObj['dropdown'].style.top = this.GetTop(suggestionListObj['element']) + suggestionListObj['element'].offsetHeight;

        /*
        // Show the iframe for IE
        if (isIE) {
        suggestionListObj['iframe'].style.top = __AutoComplete[id]['dropdown'].style.top;
        suggestionListObj['iframe'].style.left = __AutoComplete[id]['dropdown'].style.left;
        suggestionListObj['iframe'].style.width = __AutoComplete[id]['dropdown'].offsetWidth;
        suggestionListObj['iframe'].style.height = __AutoComplete[id]['dropdown'].offsetHeight;

        suggestionListObj['iframe'].style.visibility = 'visible';
        }*/


        // Show dropdown
        if (!suggestionListObj['isVisible']) {
            suggestionListObj['dropdown'].style.visibility = 'visible';
            suggestionListObj['isVisible'] = true;
        }


        // If now showing less items than before, reset the highlighted value
        if (suggestionListObj['dropdown'].childNodes.length != numItems) {
            suggestionListObj['highlighted'] = null;
        }
    },


    /**
    * Hides the dropdown layer
    * 
    * @param string id The form elements id. Used to identify the correct dropdown.
    */

    HideDropdown: function (suggestionListObj) {
        /*if (__AutoComplete[id]['iframe']) {
        __AutoComplete[id]['iframe'].style.visibility = 'hidden';
        }*/


        suggestionListObj['dropdown'].style.visibility = 'hidden';
        suggestionListObj['highlighted'] = null;
        suggestionListObj['isVisible'] = false;
    },


    /**
    * Hides all dropdowns
    */
    /*
    HideAll: function() {
    for (id in suggestionListObj) {
    this.HideDropdown(id);
    }
    },*/


    /**
    * Highlights a specific item
    * 
    * @param string id    The form elements id. Used to identify the correct dropdown.
    * @param int    index The index of the element in the dropdown to highlight
    */

    HighlightItem: function (suggestionListObj, index) {
        if (suggestionListObj['dropdown'].childNodes[index]) {
            for (var i = 0; i < suggestionListObj['dropdown'].childNodes.length; ++i) {
                if (suggestionListObj['dropdown'].childNodes[i].className == 'autocomplete_item_highlighted') {
                    suggestionListObj['dropdown'].childNodes[i].className = 'autocomplete_item';
                }
            }

            suggestionListObj['dropdown'].childNodes[index].className = 'autocomplete_item_highlighted';
            suggestionListObj['highlighted'] = index;
        }
    },


    /**
    * Highlights the menu item with the given index
    * 
    * @param string id    The form elements id. Used to identify the correct dropdown.
    * @param int    index The index of the element in the dropdown to highlight
    */

    Highlight: function (suggestionListObj, index) {
        // Out of bounds checking
        if (index == 1 && suggestionListObj['highlighted'] == suggestionListObj['dropdown'].childNodes.length - 1) {
            suggestionListObj['dropdown'].childNodes[suggestionListObj['highlighted']].className = 'autocomplete_item';
            suggestionListObj['highlighted'] = null;

        } else if (index == -1 && suggestionListObj['highlighted'] == 0) {
            suggestionListObj['dropdown'].childNodes[0].className = 'autocomplete_item';
            suggestionListObj['highlighted'] = suggestionListObj['dropdown'].childNodes.length;
        }

        // Nothing highlighted at the moment
        if (suggestionListObj['highlighted'] == null) {
            suggestionListObj['dropdown'].childNodes[0].className = 'autocomplete_item_highlighted';
            suggestionListObj['highlighted'] = 0;

        } else {
            if (suggestionListObj['dropdown'].childNodes[suggestionListObj['highlighted']]) {
                suggestionListObj['dropdown'].childNodes[suggestionListObj['highlighted']].className = 'autocomplete_item';
            }

            var newIndex = suggestionListObj['highlighted'] + index;

            if (suggestionListObj['dropdown'].childNodes[newIndex]) {
                suggestionListObj['dropdown'].childNodes[newIndex].className = 'autocomplete_item_highlighted';

                suggestionListObj['highlighted'] = newIndex;
            }
        }
    },


    /**
    * Sets the input to a given value
    * 
    * @param string id    The form elements id. Used to identify the correct dropdown.
    */

    SetValue: function (suggestionListObj) {
        suggestionListObj['element'].value = suggestionListObj['dropdown'].childNodes[suggestionListObj['highlighted']].innerHTML;
    },


    /**
    * Checks if the dropdown needs scrolling
    * 
    * @param string id    The form elements id. Used to identify the correct dropdown.
    */

    ScrollCheck: function (suggestionListObj) {
        // Scroll down, or wrapping around from scroll up
        if (suggestionListObj['highlighted'] > suggestionListObj['lastItemShowing']) {
            suggestionListObj['firstItemShowing'] = suggestionListObj['highlighted'] - (suggestionListObj['maxitems'] - 1);
            suggestionListObj['lastItemShowing'] = suggestionListObj['highlighted'];
        }

        // Scroll up, or wrapping around from scroll down
        if (suggestionListObj['highlighted'] < suggestionListObj['firstItemShowing']) {
            suggestionListObj['firstItemShowing'] = suggestionListObj['highlighted'];
            suggestionListObj['lastItemShowing'] = suggestionListObj['highlighted'] + (suggestionListObj['maxitems'] - 1);
        }

        suggestionListObj['dropdown'].scrollTop = suggestionListObj['firstItemShowing'] * 15;
    },


    /**
    * Function which handles the keypress event
    * 
    * @param string id    The form elements id. Used to identify the correct dropdown.
    */

    KeyDown: function (suggestionListObj) {
        /*// Mozilla
        if (arguments[1] != null) {
        event = arguments[1];
        }*/

        var keyCode = event.keyCode;

        switch (keyCode) {
            // Return/Enter   
            case 13:
                if (suggestionListObj['highlighted'] != null) {
                    this.SetValue(suggestionListObj);
                    this.HideDropdown(suggestionListObj);
                }

                event.returnValue = false;
                event.cancelBubble = true;
                break;
            // Escape   
            case 27:
                this.HideDropdown(suggestionListObj);
                event.returnValue = false;
                event.cancelBubble = true;
                break;
            // Up arrow   
            case 38:
                if (!suggestionListObj['isVisible']) {
                    this.ShowDropdown(suggestionListObj);
                }

                this.Highlight(suggestionListObj, -1);
                this.ScrollCheck(suggestionListObj, -1);
                return false;
                break;
            // Tab   
            case 9:
                if (suggestionListObj['isVisible']) {
                    this.HideDropdown(suggestionListObj);
                }
                return;
                // Down arrow
            case 40:
                if (!suggestionListObj['isVisible']) {
                    this.ShowDropdown(suggestionListObj);
                }

                this.Highlight(suggestionListObj, 1);
                this.ScrollCheck(suggestionListObj, 1);
                return false;
                break;
        }
    },


    /**
    * Function which handles the keyup event
    * 
    * @param string id    The form elements id. Used to identify the correct dropdown.
    */

    KeyUp: function (suggestionListObj) {
        /*// Mozilla
        if (arguments[1] != null) {
        event = arguments[1];
        }*/

        var keyCode = event.keyCode;

        switch (keyCode) {
            case 13:
                event.returnValue = false;
                event.cancelBubble = true;
                break;
            case 27:
                this.HideDropdown(suggestionListObj);
                event.returnValue = false;
                event.cancelBubble = true;
                break;
            case 38:
            case 40:
                return false;
                break;
            default:
                this.ShowDropdown(suggestionListObj);
                break;
        }
    },

    /**
    * Returns whether the dropdown is visible
    * 
    * @param string id    The form elements id. Used to identify the correct dropdown.
    */

    isVisible: function (suggestionListObj) {
        return suggestionListObj['dropdown'].style.visibility == 'visible';
    }
}