const path = require("path");

const APP_DIR = path.resolve(__dirname, "./src/");

const purgecss = require('@fullhuman/postcss-purgecss')({

  // Specify the paths to all of the template files in your project
  content: [
    './src/**/*.html',
    './src/**/*.vue',
    './src/**/*.jsx',
    // etc.
  ],

  // Include any special characters you're using in this regular expression
  defaultExtractor: content => content.match(/[\w-/:]+(?<!:)/g) || []
})

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
	optimization: {
		splitChunks: {
		  	cacheGroups: {
				styles: {
					name: 'styles',
					test: /\.css$/,
					chunks: 'all',
					enforce: true
				}
		  	}
		}
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
                                purgecss
                            ],
                        },
                    }
                ]
            }
        ]
	}
};
