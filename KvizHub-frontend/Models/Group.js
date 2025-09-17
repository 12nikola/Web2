class Group {
  constructor(id, title) {
    this.id = id;
    this.title = title;
  }

  static createSingle(data) {
    return new Group(data.categoryID, data.name);
  }

  static createList(dataArray) {
    return dataArray.map((d) => Group.createSingle(d));
  }
}

export default Group;
