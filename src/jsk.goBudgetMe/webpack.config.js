"use strict";

module.exports = {
    entry: [
        "babel-polyfill",
        //"./wwwroot/js/site.jsx",
        "./wwwroot/js/main.jsx",
    ],
    output: {
        path: "./wwwroot/dist",
        filename: "[name].js"
    },
    devServer: {
        contentBase: ".",
        host: "localhost",
        port: 9000
    },
    module: {
        loaders: [
            {
                test: /\.jsx?$/,
                exclude: /(node_modules|bower_components)/,
                loader: 'babel',
                query: {
                    presets: ['es2015', 'react']
                }
            }
        ]
    }
};