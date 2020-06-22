angular.module('WebsitesRacing', []).controller('HomeController', function ($scope, $http, $interval) {
    $scope.Websites = [
        {
            Name: "https://www.google.com/",
            CurrentWidth: 5
        },
        {
            Name: "https://stackoverflow.com/",
            CurrentWidth: 5
        },
        {
            Name: "https://www.microsoft.com/",
            CurrentWidth: 5
        },
        {
            Name: "https://www.youtube.com/",
            CurrentWidth: 5
        },
        {
            Name: "https://www.facebook.com/",
            CurrentWidth: 5
        },
    ];

    //get called when user submits the form
    $scope.submitForm = function () {
        //Reset websites loading bar
        angular.forEach($scope.Websites, function (site) {
            site.CurrentWidth = 5;
            site.Result = '';
        });

        //$http service that send or receive data from the remote server
        $http({
            method: 'POST',
            url: '/Home/LoadWebsites',
            data: $scope.Websites
        }).then(function onSuccess(data) {
            $scope.Websites = data.data;

            angular.forEach($scope.Websites, function (site) {
                if (site.IsLoaded) {
                    var currentWidth = site.CurrentWidth;
                    $interval(function () {
                        if (currentWidth < 100) {
                            currentWidth++;
                            site.CurrentWidth = currentWidth;
                        }
                        else {
                            site.DisplayResult = site.Result;
                        }
                    }, site.LoadingTime / 50);
                } else {
                    site.DisplayResult = site.Result;
                }
            });


        }).catch(function onError(data, status, headers, config) {
            $scope.errors = [];
        });
    }

    $scope.AddWebsite = function () {
        var newsite = {
            Name: "",
            CurrentWidth: 5
        };
        $scope.Websites.push(newsite);
    }

    $scope.RemoveWebsite = function (site) {
        var index = $scope.Websites.indexOf(site);
        $scope.Websites.splice(index, 1);    
    }
}).config(function ($locationProvider) {
    //default = 'false'
    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    });
});

