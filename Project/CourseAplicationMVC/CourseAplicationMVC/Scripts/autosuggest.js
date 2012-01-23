$(document).ready(function ()
{
    var xhr = new XMLHttpRequest();
    xhr.open("GET", "/Search/FindAllFucNames");
    xhr.onreadystatechange = function ()
    {
        if (xhr.readyState == 4 && xhr.status == 200)
            Autosuggest.Initialize(JSON.parse(xhr.responseText));
    };
    xhr.send(null);
   
});

Autosuggest =
{
    Initialize: function (fuclist)
    {
        var suggestionListObj = 
        {
            'data': fuclist,
            'isVisible': false,
            'element': document.getElementById('searchQuery'),
            'dropdown': null,
            'highlighted': null
        };

        suggestionListObj['element'].setAttribute('autocomplete', 'off'); //inibir atributo autocomplete de tag input
        suggestionListObj['element'].onkeydown = function (e) { return Autosuggest.KeyDown(suggestionListObj, e); };
        suggestionListObj['element'].onkeyup = function (e) { return Autosuggest.KeyUp(suggestionListObj, e); };
        suggestionListObj['element'].onkeypress = function (e)
        {
            if (!e) e = window.event;
            if (e.keyCode == 13) return false;
        };
        
        suggestionListObj['element'].ondblclick = function () { Autosuggest.ShowDropdown(suggestionListObj); };
        
        suggestionListObj['element'].onclick = function (e)
        {
            if (!e) e = window.event;
            e.cancelBubble = true;
            e.returnValue = false;
            this.value = '';
        };

        //esconder o menu de dropdown quando página clicada
        var docClick = function () 
        {
            Autosuggest.HideDropdown(suggestionListObj);
            if (suggestionListObj['element'].value == '')
                suggestionListObj['element'].value = 'Search';
        };

        if (document.addEventListener)
            document.addEventListener('click', docClick, false);
        else if (document.attachEvent)
            document.attachEvent('onclick', docClick, false);

        Autosuggest.CreateDropdown(suggestionListObj);
    },


    CreateDropdown: function (suggestionListObj)
    {
        suggestionListObj['dropdown'] = document.createElement('div');
        suggestionListObj['dropdown'].className = 'autocomplete';
        suggestionListObj['element'].parentNode.appendChild(suggestionListObj['dropdown']);
        suggestionListObj['dropdown'].style.zIndex = '10';
        suggestionListObj['dropdown'].style.visibility = 'hidden';
    },


    ShowDropdown: function (suggestionListObj)
    {

        this.HideDropdown(suggestionListObj);
        var value = suggestionListObj['element'].value;
        var toDisplay = new Array();
        var newDiv = null;
        var text = null;
        var numItems = suggestionListObj['dropdown'].childNodes.length;

        //remover nós do menu dropdown para buscar novas ocorrências
        while (suggestionListObj['dropdown'].childNodes.length > 0)
            suggestionListObj['dropdown'].removeChild(suggestionListObj['dropdown'].childNodes[0]);


        //percorrer lista de FUCs para encontrar ocorrências contendo characteres inseridos
        for (var i = 0; i < suggestionListObj['data'].length; ++i)
        {
            if (suggestionListObj['data'][i].toLowerCase().indexOf(value.toLowerCase()) >= 0)
                toDisplay[toDisplay.length] = suggestionListObj['data'][i];
        }

        //caso não encontre ocorrências
        if (toDisplay.length == 0)
        {
            this.HideDropdown(suggestionListObj);
            return;
        }


        //adicionar dados encontrados criando novo div para a sua apresentação
        for (i = 0; i < toDisplay.length; ++i)
        {
            newDiv = document.createElement('div');
            newDiv.className = 'autocomplete_item';
            newDiv.setAttribute('id', 'autocomplete_item_' + i);
            newDiv.setAttribute('index', i);
            newDiv.style.zIndex = '10';

            newDiv.onmouseover = function ()
            {
                Autosuggest.HighlightItem(suggestionListObj, this.getAttribute('index'));
            };

            newDiv.onclick = function ()
            {
                Autosuggest.SetValue(suggestionListObj);
                Autosuggest.HideDropdown(suggestionListObj);
            };

            text = document.createTextNode(toDisplay[i]);
            newDiv.appendChild(text);

            suggestionListObj['dropdown'].appendChild(newDiv);
        }

        //colocar visível menu de dropdown
        if (!suggestionListObj['isVisible'])
        {
            suggestionListObj['dropdown'].style.visibility = 'visible';
            suggestionListObj['isVisible'] = true;
        }
    },


    HideDropdown: function (suggestionListObj)
    {
        suggestionListObj['dropdown'].style.visibility = 'hidden';
        suggestionListObj['highlighted'] = null;
        suggestionListObj['isVisible'] = false;
    },


    HighlightItem: function (suggestionListObj, idx)
    {
        if (suggestionListObj['dropdown'].childNodes[idx])
        {
            for (var i = 0; i < suggestionListObj['dropdown'].childNodes.length; ++i)
            {
                if (suggestionListObj['dropdown'].childNodes[i].className == 'autocomplete_item_highlighted')
                    suggestionListObj['dropdown'].childNodes[i].className = 'autocomplete_item';
            }

            suggestionListObj['dropdown'].childNodes[idx].className = 'autocomplete_item_highlighted';
            suggestionListObj['highlighted'] = idx;
        }
    },


    Highlight: function (suggestionListObj, index)
    {
        //verificação de sanidade (out of bounds)
        if (index == 1 && suggestionListObj['highlighted'] == suggestionListObj['dropdown'].childNodes.length - 1)
        {
            suggestionListObj['dropdown'].childNodes[suggestionListObj['highlighted']].className = 'autocomplete_item';
            suggestionListObj['highlighted'] = null;

        }   else if (index == -1 && suggestionListObj['highlighted'] == 0)
            {
                suggestionListObj['dropdown'].childNodes[0].className = 'autocomplete_item';
                suggestionListObj['highlighted'] = suggestionListObj['dropdown'].childNodes.length;
            }

        //quando nenhum item está 'highlighted'
        if (suggestionListObj['highlighted'] == null)
        {
            suggestionListObj['dropdown'].childNodes[0].className = 'autocomplete_item_highlighted';
            suggestionListObj['highlighted'] = 0;

        } else
            {
                if (suggestionListObj['dropdown'].childNodes[suggestionListObj['highlighted']])
                    suggestionListObj['dropdown'].childNodes[suggestionListObj['highlighted']].className = 'autocomplete_item';

                var newIndex = suggestionListObj['highlighted'] + index;

                if (suggestionListObj['dropdown'].childNodes[newIndex])
                {
                    suggestionListObj['dropdown'].childNodes[newIndex].className = 'autocomplete_item_highlighted';
                    suggestionListObj['highlighted'] = newIndex;
                }
            }
    },


    SetValue: function (suggestionListObj)
    {
        suggestionListObj['element'].value = suggestionListObj['dropdown'].childNodes[suggestionListObj['highlighted']].innerHTML;
    },


    KeyDown: function (suggestionListObj)
    {

        var keyCode = event.keyCode;

        switch (keyCode)
        {

            // Tab          
            case 9:
                if (suggestionListObj['isVisible'] || suggestionListObj['highlighted'] != null)
                {
                    this.SetValue(suggestionListObj);
                    this.HideDropdown(suggestionListObj);
                }
                break;

            // Enter         
            case 13:
                if (suggestionListObj['highlighted'] != null)
                {
                    this.SetValue(suggestionListObj);
                    this.HideDropdown(suggestionListObj);
                }

                event.returnValue = false;
                event.cancelBubble = true;
                break;

            // Esc         
            case 27:
                this.HideDropdown(suggestionListObj);
                event.returnValue = false;
                event.cancelBubble = true;
                break;

            // Seta para cima         
            case 38:
                if (!suggestionListObj['isVisible'])
                    this.ShowDropdown(suggestionListObj);

                this.Highlight(suggestionListObj, -1);
                this.ScrollCheck(suggestionListObj, -1);
                break;


            // Seta para baixo 
            case 40:
                if (!suggestionListObj['isVisible'])
                    this.ShowDropdown(suggestionListObj);                

                this.Highlight(suggestionListObj, 1);
                this.ScrollCheck(suggestionListObj, 1);
                break;
        }
    },


    KeyUp: function (suggestionListObj)
    {

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
                break;

            default:
                this.ShowDropdown(suggestionListObj);
                break;
        }
    },


    isVisible: function (suggestionListObj)
    {
        return suggestionListObj['dropdown'].style.visibility == 'visible';
    }
}