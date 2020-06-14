var HtmlWebpackPlugin = require('html-webpack-plugin');

module.exports = {
    mode: 'development',
    resolve: {
        extensions: [".ts", ".tsx", ".js", ".jsx"]
    },
    module: {
        rules: [
            {
                test: /\.(t|j)sx?$/, 
                use: { loader: 'ts-loader' }, 
                exclude: /node_modules/ 
            },
            {
                enforce: "pre", 
                test: /\.js$/, 
                exclude: /node_modules/, 
                loader: "source-map-loader" 
            },
            {
                test: /\.css$/,
                use: [
                  // style-loader
                  { loader: 'style-loader' },
                  // css-loader
                  {
                    loader: 'css-loader',
                    options: {
                      modules: true
                    }
                  }
                ]
              }
        ]
    },
    plugins: [new HtmlWebpackPlugin({
        template: './src/index.html'
    })],
    devServer: {
        historyApiFallback: true
    },
    externals: {
        // global app config object
        config: JSON.stringify({
            apiUrl: 'http://localhost:8000/api/v1'
        })
    },
    devtool: "source-map"
}