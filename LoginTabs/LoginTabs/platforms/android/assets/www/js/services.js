var host = 'http://localhost:50610/api/';

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
                url: host + 'movil/Login',
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

.service('CategoriaService', function ($http) {

    var catService = {};

    catService.categorias = [];
        var req = {
            method: 'GET',
            url: host + 'movil/getCategorias',
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

.service('EmpresasService', function ($http) {

    var empService = {};
    empService.empresas = [];

    empService.getEmpresas = function (id) {
        empService.empresas = [];
        var reqe = {
            method: 'GET',
            url: host + 'movil/getEmpresas/' + id,
            headers: {
                'Content-Type': 'application/json'
            }
        }

        $http(reqe).then(function (response) {
            if (response.data != null) {                
                for (var i = 0; i < response.data.length; i++) {
                    empService.empresas.push(response.data[i]);
                }
            }
        });
        return empService.empresas;
    };
    return empService;
})

.service('ProfesionalessasService', function ($http) {

    var proService = {};
    proService.profesionales = [];

    proService.getProfesionales = function (id) {
        proService.profesionales = [];
        var reqe = {
            method: 'GET',
            url: host + 'movil/getProfesionales/' + id,
            headers: {
                'Content-Type': 'application/json'
            }
        }

        $http(reqe).then(function (response) {
            if (response.data != null) {
                for (var i = 0; i < response.data.length; i++) {
                    proService.profesionales.push(response.data[i]);
                }
            }
        });
        return proService.profesionales;
    };
    return proService;
})

.service('ServiciosService', function ($http) {

    var serService = {};
    serService.servicios = [];

    serService.getServicios = function (id) {
        serService.servicios = [];
        var reqe = {
            method: 'GET',
            url: host + 'movil/getServicios/' + id,
            headers: {
                'Content-Type': 'application/json'
            }
        }

        $http(reqe).then(function (response) {
            if (response.data != null) {
                for (var i = 0; i < response.data.length; i++) {
                    serService.servicios.push(response.data[i]);
                }
            }
        });
        return serService.servicios;
    };
    return serService;
})

.service('EmpresaService', function ($http) {

    var empreService = {};
    empreService.Empresa = {};

    empreService.getEmpresa = function (id) {
        empreService.Empresa = {};
        var reqe = {
            method: 'GET',
            url: host + 'movil/getEmpresa/' + id,
            headers: {
                'Content-Type': 'application/json'
            }
        }

        $http(reqe).then(function (response) {
            empreService.Empresa.push(response.data[i]);
        });
        return empreService.Empresa;
    };
    return empreService;
})
