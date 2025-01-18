// Essas são as funções que foram usadas na tela de scripts no Blip Builder

function run(corpo) {

    const payload = {
        itemType: "application/vnd.lime.document-select+json",
        items: buildItemsFromResponse(corpo)
    };

    return JSON.stringify(payload, null, 4);
}

// Formata a lista de itens esperada para o payload, com base na resposta da api
function buildItemsFromResponse(response) {

    var json = JSON.parse(response);
    var items = [];

    for (var x = 0; x < json.length; x++) {

        var item = {
            header: {
                type: "application/vnd.lime.media-link+json",
                value: {
                    title: json[x].title,
                    text: json[x].subtitle,
                    type: "image/jpeg",
                    uri: json[x].avatarUrl
                }
            }
        }

        items.push(item);
    }

    return items;
}