export const loadData = () => ({
    type: 'NEWS_LOADALL',
    remote: {
        url: '/api/news/takeall',
        type: 'get',
    }
});
