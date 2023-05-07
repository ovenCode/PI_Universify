const { createProxyMiddleware } = require('http-proxy-middleware');

const context = [
    "/api"
];

module.exports = function (app) {
    const appProxy = createProxyMiddleware(context, {
        target: 'https://localhost:7088',
        secure: false
    });

    /*app.use(
        createProxyMiddleware({
            target: 'https://localhost/api',
            changeOrigin: true,
            logger: console,
        })
    );

    app.listen(7088);*/

    app.use(appProxy);
};
