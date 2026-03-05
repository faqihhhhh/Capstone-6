export interface Category {
  id?: string
  name: string
  description: string
  parentId?: string | undefined
  deafLevel?: number
  index?: number
  statusCode?: number | undefined
}
