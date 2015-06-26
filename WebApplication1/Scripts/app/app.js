
angular.module('app', []).controller('GreetingController', ['$scope', function($scope) {

    $scope.browseAvaterImage = function () {

        $("#dvAvaterErrorMessage").hide();
        $('#fileUploadAvaterImage').after($('#fileUploadAvaterImage').clone(true)).remove();
        $('#imgAvaterImage').removeAttr("style");
        $('#imgAvaterImage').removeAttr("src");
        if (typeof Avaterjcrop_api !== "undefined") {
            Avaterjcrop_api.destroy();
        }

        $('#fileUploadAvaterImage').click();
    }
}]);


