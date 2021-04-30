const path = require('path');

const {
    addAfterLoader,
    removeLoaders,
    loaderByName,
    getLoaders,
    throwUnexpectedConfigError,
} = require('@craco/craco');

const throwError = (message) =>
    throwUnexpectedConfigError({
        packageName: 'craco',
        githubRepo: 'gsoft-inc/craco',
        message,
        githubIssueQuery: 'webpack',
    });

module.exports = {
    webpack: {
        configure: (webpackConfig, { paths }) => {

            paths.appBuild = webpackConfig.output.path = path.resolve('../client/phone');
            return webpackConfig;
        },
    },
    style: {
        postcss: {
            plugins: [
                require('tailwindcss'),
                require('autoprefixer'),
            ],
        },
    },
    devServer: (devServerConfig) => {
        devServerConfig.writeToDisk = true;
        return devServerConfig;
    },
};