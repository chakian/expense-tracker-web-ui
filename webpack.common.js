const path = require('path')
const HtmlWebPackPlugin = require('html-webpack-plugin')

module.exports = {
  entry: {
    index: './src/index.tsx'
  },
  output: {
    filename: 'main.js',
    path: path.resolve(__dirname, 'build'),
    publicPath: '/'
  },
  module: {
    rules: [
      {
        test: /.tsx?$/,
        use: { loader: 'ts-loader' },
        //use: 'awesome-typescript-loader',
        exclude: /node_modules/
      },
      {
        test: /.html$/,
        use: 'html-loader'
      },
      {
        test: /\.css$/,
        use: [
            { loader: 'style-loader' },
            { loader: 'css-loader' }
        ]
      }
    ]
  },
  resolve: {
    extensions: ['.css', '.tsx', '.ts', '.js'],
    modules: [path.resolve(__dirname, 'src'), 'node_modules']
  },
  plugins: [
    new HtmlWebPackPlugin({
      template: './src/index.html',
      filename: './index.html'
    })
  ]
}
