const merge = require('webpack-merge');
const common = require('./webpack.common.js');

const HtmlWebpackPlugin = require("html-webpack-plugin");
const HtmlWebpackRootPlugin = require("html-webpack-root-plugin");
const path = require("path");

module.exports = merge(common, {
	mode: "production",
	output: {
        path: path.resolve(__dirname, "../client/cef/"),
    },
	plugins: [
		new HtmlWebpackPlugin({ title: "eXo UI", hash: true }),
		new HtmlWebpackRootPlugin()
	],
});
