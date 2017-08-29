var pneuMalik;
(function (pneuMalik) {
    "use strict";
    var RequestHelper = (function () {
        function RequestHelper(method, url, headers, params, options) {
            this.method = method;
            this.url = url;
            this.headers = headers;
            this.params = params;
            this.options = options;
            this._method = method;
            this._url = url;
            this._headers = headers;
            this._params = params;
            this._options = options || {};
        }
        RequestHelper.prototype.makeRequest = function (token) {
            var _this = this;
            return new Promise(function (resolve, reject) {
                // token is for testing purposes to locate, where is method currently called from
                // console.log(token);
                _this._xhr = new XMLHttpRequest();
                _this._xhr.timeout = _this._options.timeout || 30000;
                _this._xhr.open(_this._method, _this._url);
                _this._xhr.onload = (function () {
                    if (_this._xhr.status >= 200 && _this._xhr.status < 300) {
                        resolve(_this._xhr.response);
                    }
                    else {
                        reject({
                            status: _this._xhr.status,
                            statusText: _this._xhr.statusText,
                            message: _this._xhr.response
                        });
                    }
                });
                _this._xhr.onerror = (function () {
                    reject({
                        status: _this._xhr.status,
                        statusText: _this._xhr.statusText,
                        message: _this._xhr.response
                    });
                });
                _this._xhr.ontimeout = (function () {
                    reject({
                        status: _this._xhr.status,
                        statusText: RequestHelperStatus.Timeout,
                        message: 'The request has timed out.'
                    });
                });
                _this._xhr.onabort = (function () {
                    reject({
                        status: _this._xhr.status,
                        statusText: RequestHelperStatus.Abort,
                        message: 'The request was cancelled by user.'
                    });
                });
                if (_this._headers) {
                    Object.keys(_this._headers).forEach(function (key) {
                        _this._xhr.setRequestHeader(key, _this._headers[key]);
                    });
                }
                var params = _this._params;
                // we'll need to stringify if we've been given an object
                // if we have a string, this is skipped.
                if (params && typeof params === "object") {
                    params = Object.keys(params).map(function (key) {
                        return encodeURIComponent(key) + "=" + encodeURIComponent(params[key]);
                    }).join("&");
                }
                _this._xhr.send(params);
            });
        };
        RequestHelper.prototype.cancelRequest = function () {
            if (this._xhr) {
                this._xhr.abort();
            }
        };
        return RequestHelper;
    }());
    pneuMalik.RequestHelper = RequestHelper;
    var RequestHelperStatus = (function () {
        function RequestHelperStatus() {
        }
        return RequestHelperStatus;
    }());
    RequestHelperStatus.Abort = 'abort';
    RequestHelperStatus.Timeout = 'timeout';
    pneuMalik.RequestHelperStatus = RequestHelperStatus;
})(pneuMalik || (pneuMalik = {}));
