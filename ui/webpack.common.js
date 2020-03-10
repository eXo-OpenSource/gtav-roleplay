const path = require("path");

const APP_DIR = path.resolve(__dirname, "./src/");

module.exports = {
    devtool: "source-map",
	entry: "./src/index.jsx",
	output: {
		filename: "[name].bundle.js",
		chunkFilename: '[name].bundle.js',
	},
    resolve: {
        extensions: [".js", ".jsx"]
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
            },
            {
                test: /\.(css)$/,
                use: [
                    {
                        loader: 'style-loader',
                    },
                    {
                        loader: 'css-loader',
                        options: {importLoaders: 1},
                    },
                    {
                        loader: 'postcss-loader',
                        options: {
                            ident: 'postcss',
                            plugins: [
                                require('tailwindcss'),
                                require('autoprefixer'),
                            ],
                        },
                    }
                ]
            }
        ]
    }
};
