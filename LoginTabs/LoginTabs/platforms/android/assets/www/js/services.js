var host = 'http://localhost:81/api/';

angular.module('starter.services', [])

.factory('Chats', function() {
  // Might use a resource here that returns a JSON array

  // Some fake testing data
  var chats = [{
    id: 0,
    name: 'Ben Sparrow',
    lastText: 'You on your way?',
    face: 'img/ben.png'
  }, {
    id: 1,
    name: 'Max Lynx',
    lastText: 'Hey, it\'s me',
    face: 'img/max.png'
  }, {
    id: 2,
    name: 'Adam Bradleyson',
    lastText: 'I should buy a boat',
    face: 'img/adam.jpg'
  }, {
    id: 3,
    name: 'Perry Governor',
    lastText: 'Look at my mukluks!',
    face: 'img/perry.png'
  }, {
    id: 4,
    name: 'Mike Harrington',
    lastText: 'This is wicked good ice cream.',
    face: 'img/mike.png'
  }];

  return {
    all: function() {
      return chats;
    },
    remove: function(chat) {
      chats.splice(chats.indexOf(chat), 1);
    },
    get: function(chatId) {
      for (var i = 0; i < chats.length; i++) {
        if (chats[i].id === parseInt(chatId)) {
          return chats[i];
        }
      }
      return null;
    }
  };
})

.service('LoginService', function ($q, $http) {
    return {
        loginUser: function (name, pw) {
            var defered = $q.defer();
            var promise = defered.promise;

            var req = {
                method: 'POST',
                url: host + 'Usuarios/Login',
                headers: {
                    'Content-Type': 'application/json'
                },
                data: { Email: name, Password: pw }
            }

            $http(req).success(function (respuesta) {
                defered.resolve(respuesta);
            }).error (function (err) {
                defered.reject('Wrong credentials.');
            });
            return promise;
        }
    }
})

.service('CategoriaService', function ($q, $http) {

    var catService = {};

    catService.categorias = [];
        var req = {
            method: 'GET',
            url: 'http://localhost:50610/api/categorias/getCategorias',
            headers: {
                'Content-Type': 'application/json'
            }
        }

        $http(req).then(function (response) {
            for (var i = 0; i < response.data.length; i++) {
                catService.categorias.push(response.data[i]);
            }
        });

        catService.getCats = function () {
            return catService.categorias;
        };
        return catService;
})

