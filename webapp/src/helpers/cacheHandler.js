import LRUCache from "lru-cache";

class Cache {
    static cache;

    static init() {
        Cache.cache = new LRUCache({
            maxAge: 1000 * 60 * 60 * 24,
        });
    }

    static get(key) {
        return Cache.cache.get(key);
    }

    static set(key, value) {
        Cache.cache.set(key, value);
    }

    static del(key) {
        Cache.cache.del(key);
    }

    static clear() {
        Cache.cache.reset();
    }

    static clearSameUrl(url) {
        Cache.cache.forEach((value, key) => {
            if (key.toLowerCase().includes(url.toLowerCase())) {
                Cache.del(key);
            }
        });
    }
}

export default Cache;