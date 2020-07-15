const webpack = require('webpack')
const { merge } = require('webpack-merge')
const UglifyJSPlugin = require('uglifyjs-webpack-plugin')
const common = require('./webpack.common.js')

module.exports = merge(common, {
  mode: 'production',
  devtool: 'source-map',
  plugins: [
    new UglifyJSPlugin({
      sourceMap: true
    }),
  ],
  externals: {
    Config: JSON.stringify({
      apiUrl: 'https://expense-track-api.appspot.com/api/v1'
    })
  }
})