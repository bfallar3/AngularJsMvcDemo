(function () {
    /*
     * define the main application module.
     */
    var main = angular.module('main', ['ngRoute']);

    /*
     * configure the routes for SPA.      
     */
    main.config(['$routeProvider', function ($routeProvider) {
        $routeProvider
            .when('/courses', {
                templateUrl: 'Templates/courses.html',
                controller: 'courseController'
            })
            .when('/addCourse', {
                templateUrl: 'Templates/addcourse.html',
                controller: 'courseController'
            })
            .when('/editCourse/:id', {
                templateUrl: 'Templates/editcourse.html',
                controller: 'courseController'
            })
            .otherwise({ redirectTo: '/courses' });
    }]);

    /*
     * define the controller for the main app courses.
     */
    main.controller('courseController', function ($scope, $location, $route, $routeParams, courseFactory, courseService) {
        $scope.courses = {};
        $scope.selectedCourse = {};
        $scope.newCourseId = '';
        $scope.newCourseCode = '';
        $scope.newCourseName = '';
        $scope.addform = {};

        // remove course.
        $scope.remove = function (index, id) {
            courseFactory.remove(id).success(function () {
                $scope.courses.splice(index, 1);
            });            
        };

        // submit new course.
        $scope.submit = function () {
            if (!$scope.addform.$invalid) {
                courseFactory.add($scope.newCourseCode, $scope.newCourseName).success(function (resp) {
                    $scope.courses.push({ "CourseId": $scope.newCourseId, "CourseCode": $scope.newCourseCode, "CourseName": $scope.newCourseName });
                    $location.path('#courses');
                });                
            }
        };

        // update existing course name.
        $scope.update = function () {
            var id = $routeParams.id;
            var newName = $scope.selectedCourse.CourseName;
            var newCode = $scope.selectedCourse.CourseCode;

            courseFactory.update(id, newCode, newName).success(function () {
                for (i = 0; i < $scope.courses.length; i++) {
                    if ($scope.courses[i].CourseId === id) {
                        $scope.courses[i].CourseName = newName;
                        $scope.courses[i].CourseCode = newCode;
                        break;
                    }
                }

                $location.path('#courses');
            });
        };

        activate();

        function activate() {
            //$scope.courses = courseService.courses;
            courseFactory.query().success(function (data) {
                $scope.courses = data;
            });
        };
        
        // register handler when view is loaded.
        $scope.$on('$viewContentLoaded', function () {
            var template = $route.current.templateUrl;

            // check if 'Templates/editcourse.html' has been selected.
            // then update the selectedCourse object.
            if (template.indexOf('editcourse') !== -1) {
                var id = $routeParams.id;
                for (i = 0; i < $scope.courses.length; i++) {
                    if ($scope.courses[i].CourseId.toString() === id.toString()) {
                        $scope.selectedCourse = $scope.courses[i];
                        break;
                    }
                }
            }
        });
    });

    /*
     * singleton service to retrieve the courses into memory.
     */
    main.service('courseService', function (courseFactory) {
        //courseFactory.query().success(function (response) {
        //    $scope.courses = response;
        //});  
        //this.courses = [
        //        { "CourseId": "1", "CourseCode": "COMSCI", "CourseName": "Computer Science" },
        //        { "CourseId": "2", "CourseCode": "COE", "CourseName": "Engineering" },
        //        { "CourseId": "3", "CourseCode": "BIO", "CourseName": "Biology" },
        //];
    });

    /*
     * define a course factory to perform
     * CRUD operations for Courses.
     */
    main.factory('courseFactory', function ($http) {
        var factory = {};
        var url = 'http://localhost:16706/api/Courses';

        /*
         * post
         */
        factory.add = function (courseCode, courseName) {
            return $http.post(url, { "CourseName": courseName, "CourseCode": courseCode });
        };

        /*
         * put
         */
        factory.update = function (id, courseCode, courseName) {
            return $http.put(url + '/' + id, { "CourseCode": courseCode, "CourseName": courseName });
        };

        /*
         * delete
         */
        factory.remove = function (id) {
            return $http.delete(url + '/' + id);
        };

        /*
         * get all
         */
        factory.query = function () {
            return $http.get(url);
        };

        /*
         * get by id
         */
        factory.get = function (id) {
            return $http.get(url + '/' + id);
        };

        return factory;
    });
})();