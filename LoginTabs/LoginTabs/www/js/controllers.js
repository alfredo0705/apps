angular.module('starter.controllers', [])

.controller('DashCtrl', function ($scope) {


})

.controller('ChatsCtrl', function($scope, Chats) {
  // With the new view caching in Ionic, Controllers are only called
  // when they are recreated or on app start, instead of every page change.
  // To listen for when this page is active (for example, to refresh data),
  // listen for the $ionicView.enter event:
  //
  //$scope.$on('$ionicView.enter', function(e) {
  //});

  $scope.chats = Chats.all();
  $scope.remove = function(chat) {
    Chats.remove(chat);
  };
})

.controller('ChatDetailCtrl', function($scope, $stateParams, Chats) {
  $scope.chat = Chats.get($stateParams.chatId);
})

.controller('AccountCtrl', function($scope) {
  $scope.settings = {
    enableFriends: true
  };
})

.controller('LoginCtrl', function($scope, LoginService, $ionicPopup, $state) {
    $scope.data = {};
    $scope.elementos = [];
    $scope.login = function() {
        LoginService.loginUser($scope.data.username, $scope.data.password).then(function (respuesta) {
            var alertPopup = $ionicPopup.alert({
                title: 'Fuck you!!',
                template: 'Welcom! ' + respuesta.Nombres
            });
            $scope.elementos = respuesta;
            $state.go('tab.dash');
        }).catch(function(err) {
            var alertPopup = $ionicPopup.alert({
                title: 'Login failed!',
                template: 'Please check your credentials! ' + $scope.data.username
            });
        });
    }    
})

.controller('CategoriasCtrl', function ($scope, CategoriaService, $ionicPopup) {
    $scope.categorias = [];
    $scope.categorias = CategoriaService.getCats();
})

.controller('EmpresasCtrl', function ($scope, EmpresasService, $stateParams) {
    $scope.empresas = [];
    $scope.empresas = EmpresasService.getEmpresas($stateParams.empresaId);
})

.controller('ProfesionalesCtrl', function ($scope, ProfesionalessasService, $stateParams) {
    $scope.profesionales = [];
    $scope.profesionales = ProfesionalessasService.getProfesionales($stateParams.profesionalId);
})

.controller('ServiciosCtrl', function ($scope, ServiciosService, $stateParams) {
    $scope.servicios = [];
    $scope.servicios = ServiciosService.getServicios($stateParams.nit);
})

.controller('MapsCtrl', function ($scope, $ionicLoading) {

    $scope.info_position = {
        lat: 43.07493,
        lng: -89.381388
    };

    $scope.center_position = {
        lat: 43.07493,
        lng: -89.381388
    };

    $scope.my_location = "";

    $scope.$on('mapInitialized', function (event, map) {
        $scope.map = map;
    });

    $scope.centerOnMe = function () {
        $scope.positions = [];

        $ionicLoading.show({
            template: 'Loading...'
        });

        // with this function you can get the user’s current position
        // we use this plugin: https://github.com/apache/cordova-plugin-geolocation/
        navigator.geolocation.getCurrentPosition(function (position) {
            var pos = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
            $scope.current_position = { lat: pos.k, lng: pos.D };
            $scope.my_location = pos.k + ", " + pos.D;
            $scope.map.setCenter(pos);
            $ionicLoading.hide();
        });
    };
})
