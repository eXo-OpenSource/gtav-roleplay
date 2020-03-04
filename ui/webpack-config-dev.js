const HtmlWebpackPlugin = require('html-webpack-plugin');
const HtmlWebpackRootPlugin = require('html-webpack-root-plugin');
const path = require('path');


const APP_DIR = path.resolve(__dirname, "./src/");

module.exports = {
    devtool: 'source-map',
    mode: "development",
    watch: true,
    output: {
        path: path.resolve(__dirname, 'dist'),
        filename: "./app-bundle.js"
    },
    resolve: {
        extensions: ['.js', '.jsx']
    },
    devServer: {
        contentBase: path.join(__dirname, 'dist'),
        compress: true,
        port: 9000
    },
    module: {
        rules: [
            {
                test: /\.(jsx)$/,
                exclude: /node_modules/,
                include: APP_DIR,
                use: {
                    loader: "babel-loader"
                }
            }
        ]
    },
    plugins: [new HtmlWebpackPlugin({ title: 'eXo UI' }),
        new HtmlWebpackRootPlugin()]
};