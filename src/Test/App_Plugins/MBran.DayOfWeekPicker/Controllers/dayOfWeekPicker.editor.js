angular.module('umbraco')
    .controller('MBran.DayOfWeekPicker',
    function ($scope) {

        if (!$scope.model.value) {
            $scope.model.value = []
        }

        $scope.days = ['Sunday',
            'Monday',
            'Tuesday',
            'Wednesday',
            'Thursday',
            'Friday',
            'Saturday'];
    });