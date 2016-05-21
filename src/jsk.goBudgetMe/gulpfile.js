/// <binding />
// Include gulp
var gulp = require('gulp');

// Paths
var path = {
    webroot: "./wwwroot/"
};

path.js = path.webroot + "js/**/*.js";
path.jsx = path.js + "x";
path.jsDest = path.webroot + "js/";
path.minJs = path.webroot + "js/**/*.min.js";
path.css = path.webroot + "css/**/*.css";
path.minCss = path.webroot + "css/**/*.min.css";
path.concatJsDest = path.webroot + "js/site.min.js";
path.concatCssDest = path.webroot + "css/site.min.css";

// Include Our Plugins
var jshint = require('gulp-jshint'),
	babel = require('gulp-babel'),
	concat = require('gulp-concat'),
	rename = require('gulp-rename'),
	//sass = require('gulp-sass'),
	uglify = require('gulp-uglify');

gulp.task("react", function () {
    return gulp.src(path.jsx)
        .pipe(babel({
            plugins: ['transform-react-jsx']
        })).
        pipe(gulp.dest(path.jsDest));
});

// Lint Task
gulp.task('lint', function () {
    return gulp.src(path.js)
        .pipe(jshint())
        .pipe(jshint.reporter('default'));
});

// Concatenate & Minify JS
gulp.task('scripts', function () {
    return gulp.src(path.js)
        .pipe(concat('all.js'))
        .pipe(gulp.dest('dist'))
        .pipe(rename('all.min.js'))
        .pipe(uglify())
        .pipe(gulp.dest('dist/js'));
});

// Watch Files For Changes
gulp.task('watch', function () {
    gulp.watch(path.jsx, ['react']);
    gulp.watch(path.js, ['lint', 'scripts']);
    //gulp.watch('scss/*.scss', ['sass']);
});

// Default Task
gulp.task('default', ['react', 'lint', 'scripts', 'watch']);




















/* Compile Our Sass
gulp.task('sass', function() {
    return gulp.src('scss/*.scss')
        .pipe(sass())
        .pipe(gulp.dest('dist/css'));
});
*/