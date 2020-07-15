const webpack = require('webpack')
const { merge } = require('webpack-merge')
const common = require('./webpack.common.js')

module.exports = merge(common, {
  mode: 'development',
  devtool: 'source-map',
  externals: {
    Config: JSON.stringify({
      apiUrl: 'https://test-dot-expense-track-api.appspot.com/api/v1'
    })
  }
})