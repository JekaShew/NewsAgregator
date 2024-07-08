import initialState from "../../../../../initialState";



export default (state = initialState, action) => {
    switch (action.type) {

        case "COMMENT_SELECT":
            return {
                ...state,
                editComment:
                {
                    ...state.editComment,
                    [action.name]:
                    {
                        value: action.val,
                    },
                }
            }

        case "COMMENT_SELECT_PARAMETER":
            return {
                ...state,
                editComment:
                {
                    ...state.editComment,
                    [action.name]:
                    {

                        id: action.val.value,
                        text: action.val.text,

                    },
                }
            }

        case "COMMENT_LOAD_PARAMETERS_START":
            return {
                ...state,
                editComment:
                {
                    loading: true,
                    id: {
                        value: '',
                    },
                    text: {
                        value: '',
                    },
                    date: {
                        value: '',
                    },
                    news: {
                        value: {
                            id: '',
                            text: '',
                        },
                    },

                    account: {
                        value: {
                            id: '',
                            text: '',
                        },
                    },

                    newses: {
                        value: {
                            id: '',
                            text: '',
                        },    
                    },
                    accounts: {
                        value: {
                            id: '',
                            text: '',
                        },       
                    },
                }
            }

        case 'COMMENT_LOAD_PARAMETERS_SUCCESS':
            return {
                ...state,
                editComment:
                {
                    ...state.editComment,
                    loading: false,
                    ...Object.fromEntries(Object.entries(action.data).map(x => ([
                        x[0],
                        {

                            value: x[1],
                        }
                    ])))
                }

            }

        case "COMMENT_LOAD_START":
            return {
                ...state,
                ...state.editComment,
                editComment:
                {
                    loading: true,

                }
            }

        case "COMMENT_LOAD_SUCCESS":
            return {
                ...state,
                editComment:
                {
                    ...state.editComment,
                    ...state.editComment.accounts,
                    ...state.editComment.newses,
                    loading: false,
                    ...Object.fromEntries(Object.entries(action.data).map(x => ([
                        x[0],
                        {
                            value: x[1],
                        }
                    ]))),
                }
            }

        default:
            return state
    }
}