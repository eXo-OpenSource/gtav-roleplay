const merge = require('webpack-merge');
const common = require('./webpack.common.js');

const glob = require('glob')
const DuplicatePackageCheckerPlugin = require("duplicate-package-checker-webpack-plugin");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const HtmlWebpackRootPlugin = require("html-webpack-root-plugin");
const CompressionPlugin = require('compression-webpack-plugin');
const path = require("path");

module.exports = merge(common, {
	mode: "production",
	output: {
        path: path.resolve(__dirname, "../client/cef/"),
	},
	plugins: [
		new DuplicatePackageCheckerPlugin(),
		new HtmlWebpackPlugin({ title: "eXo UI", hash: true }),
		new HtmlWebpackRootPlugin(),
		new CompressionPlugin()
	],
	module: {
		rules: [
			{
                test: /\.(css)$/,
                use: [
                    {
						loader: 'postcss-loader',
						options: {
							ident: 'postcss',
							plugins: [
								require('tailwindcss'),
								require('autoprefixer'),
								require('@fullhuman/postcss-purgecss')({
									// Specify the paths to all of the template files in your project
									content: [
										'./src/**/*.html',
										'./src/**/*.jsx',
									],

									// Include any special characters you're using in this regular expression
									defaultExtractor: content => content.match(/[\w-/:]+(?<!:)/g) || []
								})
							],
						}
					}
                ]
            }
		]
	}
});
