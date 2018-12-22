/// <binding BeforeBuild='default' AfterBuild='default' Clean='default' ProjectOpened='default' />
module.exports = function (grunt) {
    'use strict';
    // Project configuration.
    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),
        // Sass
        sass: {
            options: {
                sourceMap: true, // Create source map
                outputStyle: 'compressed' // Minify output
            },
            dist: {
                files: [
                    {
                        expand: true, // Recursive
                        cwd: "Content/sass", // The startup directory
                        src: ["**/*.scss"], // Source files
                        dest: "Content/css", // Destination
                        ext: ".css" // File extension
                    }
                ]
            }
        }
    });
    // Load the plugin
    grunt.loadNpmTasks('grunt-sass');
    // Default task(s).
    grunt.registerTask('default', ['sass']);
};