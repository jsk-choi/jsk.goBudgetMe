"use strict";

module.exports = {
    entry: [
        "babel-polyfill",
        "./wwwroot/js/index.jsx",
    ],
    output: {
        path: "./wwwroot/dist",
        filename: "index.js"
    },
    devServer: {
        contentBase: ".",
        host: "localhost",
        port: 9000
    },
    module: {
        loaders: [
            {
                test: /\.jsx$/,
                exclude: /node_modules/,
                loader: "babel-loader",
                query: {
                    presets: ['es2015', 'react']
                }
            }
        ]
    }
};