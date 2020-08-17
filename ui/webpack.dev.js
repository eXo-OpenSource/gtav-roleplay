const merge = require('webpack-merge');
const common = require('./webpack.common.js');

const HtmlWebpackPlugin = require("html-webpack-plugin");
const HtmlWebpackRootPlugin = require("html-webpack-root-plugin");
const path = require("path");

module.exports = merge.smart(common, {
  mode: "development",
  output: {
    path: path.resolve(__dirname, "dist"),
  },
  devServer: {
    contentBase: path.join(__dirname, "dist"),
    compress: true,
    port: 8090,
    historyApiFallback: true
  },
  plugins: [
    new HtmlWebpackPlugin({ title: "eXo UI" }),
    new HtmlWebpackRootPlugin()
  ]
});
