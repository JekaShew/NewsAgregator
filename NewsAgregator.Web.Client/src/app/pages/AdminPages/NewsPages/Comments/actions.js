export const loadData = () => ({
    type: 'COMMENT_LOADALL',
    remote: {
        url: '/api/comment/takeall',
        type: 'get',
    }
});
