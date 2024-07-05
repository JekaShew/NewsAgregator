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

        case 'COMMENT_LOAD_PARAMETERS_SUCCESS':
            return {
                ...state,
                editComment:
                {
                    ...state.editComment,
                    ...Object.fromEntries(Object.entries(action.data).map(x => ([
                        x[0],
                        {
                            value: state.editComment[x[0]].value,
                            options: x[1],
                        }
                    ])))
                }

            }

        case "COMMENT_LOAD_SUCCESS":
            return {
                ...state,
                editComment:
                {
                    ...state.editComment,
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