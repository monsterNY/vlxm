/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 80013
Source Host           : localhost:3306
Source Database       : vlxm

Target Server Type    : MYSQL
Target Server Version : 80013
File Encoding         : 65001

Date: 2018-12-29 17:56:57
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for article_info
-- ----------------------------
DROP TABLE IF EXISTS `article_info`;
CREATE TABLE `article_info` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `title` varchar(255) NOT NULL COMMENT '标题',
  `userId` bigint(20) DEFAULT NULL,
  `author` varchar(255) DEFAULT NULL COMMENT '作者',
  `articleType` int(11) NOT NULL COMMENT '文章类型',
  `category` varchar(255) NOT NULL COMMENT '类别',
  `description` varchar(255) DEFAULT NULL COMMENT '描述',
  `content` text NOT NULL COMMENT '内容',
  `faceImg` varchar(255) NOT NULL DEFAULT '0' COMMENT '封面图',
  `status` int(11) NOT NULL DEFAULT '0' COMMENT '状态',
  `createTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `updateTime` datetime DEFAULT NULL,
  `publishTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '发布时间',
  `validFlag` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=38 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='文章信息';

-- ----------------------------
-- Records of article_info
-- ----------------------------
INSERT INTO `article_info` VALUES ('2', 'test', '0', 'test', '3', '3', null, '<p>test</p>', 'upload/a557d942-d9cc-4417-accf-a9b038ba7fc9.jpg', '0', '2018-12-27 17:05:43', null, '0001-01-01 00:00:00', '1');
INSERT INTO `article_info` VALUES ('3', '是白神了', '0', '小白', '3', '3', null, '<p>哼哼哈嘿</p>', 'upload/891fd9b3-cbb1-48c8-9acd-cb990ead4e89.jpg', '1', '2018-12-27 17:07:01', null, '0001-01-01 00:00:00', '1');
INSERT INTO `article_info` VALUES ('4', '厚', '0', 't', '2', '3,2', 'test', '<p>test</p>', 'upload/c49581bd-a624-43cf-ad20-53a8fad302ef.jpg', '0', '2018-12-27 17:12:44', null, '0001-01-01 00:00:00', '1');
INSERT INTO `article_info` VALUES ('6', 'e', '0', 'e', '3', '3', 'e', '<p>e</p>', 'upload/3cfaf9d1-33c2-4e1b-8d53-14f185af2442.jpg', '0', '2018-12-27 17:16:22', null, '2018-12-26 16:00:00', '1');
INSERT INTO `article_info` VALUES ('10', 'adsfa', '0', 'saf', '3', '3', 'asfasf', '<p><span style=\"font-size:30px\"><span style=\"font-family:Impact, serif\">?</span></span></p><p><span style=\"font-size:30px\"><span style=\"font-family:Impact, serif\">真逗呢</span></span></p><p><span style=\"font-size:30px\"><span style=\"font-family:Impact, serif\">哈哈哈</span></span></p>', 'upload/344f503b-3dd4-4ab4-8fcb-0121232e84ea.jpg', '0', '2018-12-27 18:09:10', null, '2018-12-26 16:00:00', '1');
INSERT INTO `article_info` VALUES ('11', '今天的我依然是满满呢', '0', '不知名的小小', '3', '3,1,2', 'em...', '<pre>今天，开始了呢</pre>', 'upload/77729c1e-1f91-4ac9-acd2-87398a9b5142.jpg', '0', '2018-12-28 09:40:04', null, '2018-12-27 16:00:00', '1');
INSERT INTO `article_info` VALUES ('12', '今天也是元气满满呢', '2018', '小小', '1', '3', '1', '<pre>一天，又开始了呢</pre>', '/upload/image/2018-12-28/2362ab9f1f854b23984ddb967bd14c9f.jpg', '0', '2018-12-28 09:45:09', null, '2018-12-27 16:00:01', '1');
INSERT INTO `article_info` VALUES ('13', '今天也是元气满满呢', '2018', '小小', '1', '3', '1', '<pre>一天，又开始了呢</pre>', '/upload/image/2018-12-28/2362ab9f1f854b23984ddb967bd14c9f.jpg', '0', '2018-12-28 09:45:09', null, '2018-12-27 16:00:01', '1');
INSERT INTO `article_info` VALUES ('14', '今天也是元气满满呢', '2018', '小小', '1', '3', '1', '<pre>一天，又开始了呢</pre>', '/upload/image/2018-12-28/2362ab9f1f854b23984ddb967bd14c9f.jpg', '0', '2018-12-28 09:45:09', null, '2018-12-27 16:00:01', '1');
INSERT INTO `article_info` VALUES ('15', '今天也是元气满满呢', '2018', '小小', '1', '3', '1', '<pre>一天，又开始了呢</pre>', '/upload/image/2018-12-28/2362ab9f1f854b23984ddb967bd14c9f.jpg', '0', '2018-12-28 09:45:09', null, '2018-12-27 16:00:01', '1');
INSERT INTO `article_info` VALUES ('16', '今天也是元气满满呢', '2018', '小小', '1', '3', '1', '<pre>一天，又开始了呢</pre>', '/upload/image/2018-12-28/2362ab9f1f854b23984ddb967bd14c9f.jpg', '0', '2018-12-28 09:45:09', null, '2018-12-27 16:00:01', '1');
INSERT INTO `article_info` VALUES ('18', '今天也是元气满满呢', '2018', '小小', '1', '3', '1', '<pre>一天，又开始了呢</pre>', '/upload/image/2018-12-28/2362ab9f1f854b23984ddb967bd14c9f.jpg', '0', '2018-12-28 09:45:09', null, '2018-12-27 16:00:01', '1');
INSERT INTO `article_info` VALUES ('19', 'ohe', '2018', 'yeah', '3', '10,11', 'doo doo doo', '<p>doo</p>', '/upload/image/2018-12-28/d9729e58db45489590599f91f6a1dbb6.jpg', '0', '2018-12-28 12:00:15', null, '2018-12-27 16:00:01', '1');
INSERT INTO `article_info` VALUES ('20', 'e', '2018', 'e', '3', '11', 'e', '<p>e</p>', '/upload/image/2018-12-28/c8922834be0d4174a5e8dd1503a83e75.jpg', '1', '2018-12-28 12:00:45', null, '2018-12-27 16:00:01', '1');
INSERT INTO `article_info` VALUES ('21', 'too happy', '2018', 'yayaya', '2', '10', 'a', '<p></p>', '/upload/image/2018-12-28/67a309989a0f4574b31ee6e496813a88.jpg', '0', '2018-12-28 14:09:02', null, '2018-12-28 06:08:52', '1');
INSERT INTO `article_info` VALUES ('22', '阿勒', '2018', '小小', '2', '11,9', '1', '<p>12</p>', '/upload/image/2018-12-28/92eb604c7fca4187bccf67819f63d66b.jpg', '0', '2018-12-28 14:12:59', null, '2018-12-28 06:12:58', '1');
INSERT INTO `article_info` VALUES ('23', '阿勒', '2018', '小小', '2', '11,9', '1', '<p>12</p>', '/upload/image/2018-12-28/92eb604c7fca4187bccf67819f63d66b.jpg', '0', '2018-12-28 14:12:59', null, '2018-12-28 06:12:58', '1');
INSERT INTO `article_info` VALUES ('24', '阿勒', '2018', '小小', '2', '11,9', '1', '<p>12</p>', '/upload/image/2018-12-28/92eb604c7fca4187bccf67819f63d66b.jpg', '0', '2018-12-28 14:12:59', null, '2018-12-28 06:12:58', '1');
INSERT INTO `article_info` VALUES ('25', '阿勒', '2018', '小小', '2', '11,9', '1', '<p>12</p>', '/upload/image/2018-12-28/92eb604c7fca4187bccf67819f63d66b.jpg', '0', '2018-12-28 14:12:59', null, '2018-12-28 06:12:58', '1');
INSERT INTO `article_info` VALUES ('26', '阿勒', '2018', '小小', '2', '11,9', '1', '<p>12</p>', '/upload/image/2018-12-28/92eb604c7fca4187bccf67819f63d66b.jpg', '0', '2018-12-28 14:12:59', null, '2018-12-28 06:12:58', '1');
INSERT INTO `article_info` VALUES ('27', '阿勒', '2018', '小小', '2', '11,9', '1', '<p>12</p>', '/upload/image/2018-12-28/92eb604c7fca4187bccf67819f63d66b.jpg', '0', '2018-12-28 14:12:59', null, '2018-12-28 06:12:58', '1');
INSERT INTO `article_info` VALUES ('29', 'doo', '2018', 'la na na ', '3', '11', '在使用reactjs库的时候，会遇到将一段html的字符串，然后要将它插入页面中以html的形式展现，然而直接插入的话页面显示的就是这段字符串，而不会进行转义，可以用一下方法插入，便可以html的形式展现：', '<pre><span style=\"font-size:14px\"><span style=\"color:#4f4f4f\"><span style=\"font-size:nanpx\"><span style=\"color:#006666\">&lt;divdangerouslySetInnerHTML=</span></span>{{__<span style=\"font-size:nanpx\">html</span>: <span style=\"font-size:nanpx\"><span style=\"color:#009900\">&quot;&lt;p&gt;内容呢&lt;/p&gt;&quot;</span></span>}}<span style=\"font-size:nanpx\"><span style=\"color:#006666\"> /&gt;</span></span></span></span><br/></pre><p>??</p><p></p><hr/><p></p><hr/><p></p><hr/><p></p><hr/><p></p><hr/><p></p><p>123123123</p><div class=\"media-wrap image-wrap\"><img src=\"blob:http://localhost:4444/d4733ff3-055e-4e54-b41c-bad2a2390313\"/></div><p>图片呢</p><div class=\"media-wrap image-wrap\"><img src=\"blob:http://localhost:4444/d4733ff3-055e-4e54-b41c-bad2a2390313\"/></div><p></p>', '/upload/image/2018-12-29/d49b76ae3d4f49b6ab0cea88923a1b99.jpg', '0', '2018-12-29 11:28:40', null, '2018-12-28 16:00:01', '1');
INSERT INTO `article_info` VALUES ('30', '12', '2018', '123', '2', '8', '123', '<p></p><div class=\"media-wrap image-wrap\"><img src=\"blob:http://localhost:4444/6bc2a390-4146-41a3-af8c-d8bce3f6d6d4\"/></div><p></p>', '/upload/image/2018-12-29/623df2f937ce414db8487e8341c54203.jpg', '0', '2018-12-29 11:43:41', null, '2018-12-29 03:43:40', '1');
INSERT INTO `article_info` VALUES ('31', '123', '2018', '123', '2', '10', '123123', '', '/upload/image/2018-12-29/eb211ac7734c467aab56f0c93b579b25.jpg', '0', '2018-12-29 11:58:59', null, '2018-12-29 03:58:58', '1');
INSERT INTO `article_info` VALUES ('32', '123123', '2018', '123', '3', '6', '123', '', '/upload/image/2018-12-29/dc132a48afa94788982baf693504a52f.jpg', '0', '2018-12-29 12:01:38', null, '2018-12-29 04:00:59', '1');
INSERT INTO `article_info` VALUES ('33', 'na', '2018', 'dooo', '2', '11', 'hi boy', '<p></p><hr/><p></p><hr/><p></p><hr/><p></p><div class=\"media-wrap image-wrap\"><img src=\"blob:http://localhost:4444/acb387eb-2810-467b-8d42-103ecc5ff993\"/></div><p></p><p>nice yeap</p><p></p><div class=\"media-wrap image-wrap\"><img src=\"blob:http://localhost:4444/5b5d2b41-c5e8-4e68-a5d3-70d2cc801fc9\"/></div><p></p>', '/upload/image/2018-12-29/01acdee1e98f4515b3097d11cc62b2b9.jpg', '0', '2018-12-29 13:45:47', null, '2018-12-29 05:45:46', '1');
INSERT INTO `article_info` VALUES ('34', '123', '2018', 'waer', '2', '10', 'ware', '<p></p><div class=\"media-wrap image-wrap\"><img src=\"blob:http://localhost:4444/2ba686d7-f014-4633-bf0a-70c2ac816c90\"/></div><p></p>', '/upload/image/2018-12-29/04ccb5c5743047d39340feaf7b7e1072.jpg', '0', '2018-12-29 13:57:57', null, '2018-12-29 05:57:56', '1');
INSERT INTO `article_info` VALUES ('35', 'd', '2018', 'd', '2', '10,11', 'sad', '', '/upload/image/2018-12-29/20eca98c72d944c69244af94d886c650.jpg', '0', '2018-12-29 15:03:02', null, '2018-12-29 07:03:01', '1');
INSERT INTO `article_info` VALUES ('36', 'asdf', '2018', 'sa', '2', '11', null, '<p>sadfdsafsafdsaf</p><hr/><p></p><hr/><p></p><hr/><p></p><div class=\"media-wrap image-wrap\"><img src=\"http://api.moreover.manage//upload/image/2018-12-29/a7722eb7f3954dd4bb4bfc78d2ce4fcc.jpg\"/></div><p></p><hr/><p>sadf</p><hr/><p>sfda</p><hr/><p>sfda</p><hr/><p></p><hr/><p></p><hr/><p></p><hr/><p></p>', '/upload/image/2018-12-29/516ced97d0f4486483bf034c61e3c612.jpg', '0', '2018-12-29 15:05:25', null, '2018-12-29 07:05:10', '1');
INSERT INTO `article_info` VALUES ('37', '123', '2018', '123', '3', '10', '123', '<p></p><div class=\"media-wrap image-wrap\"><img src=\"http://api.moreover.manage//upload/image/2018-12-29/83a471244b714cfe9ea26d1886c3ca2e.jpg\" width=\"500px\" height=\"500px\" style=\"width:500px;height:500px\"/></div><p>123</p>', '/upload/image/2018-12-29/3ce58fb1c52246fdbd99f2529162b980.jpg', '0', '2018-12-29 15:07:04', null, '2018-12-29 07:07:03', '1');

-- ----------------------------
-- Table structure for article_tag
-- ----------------------------
DROP TABLE IF EXISTS `article_tag`;
CREATE TABLE `article_tag` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `tagName` varchar(50) DEFAULT NULL,
  `createTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updateTime` datetime DEFAULT NULL,
  `validFlag` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='文章标签';

-- ----------------------------
-- Records of article_tag
-- ----------------------------
INSERT INTO `article_tag` VALUES ('1', '青春', '2018-12-27 15:07:10', null, '1');
INSERT INTO `article_tag` VALUES ('2', '校园', '2018-12-27 15:07:18', null, '1');
INSERT INTO `article_tag` VALUES ('3', '日常', '2018-12-27 15:07:22', null, '1');
INSERT INTO `article_tag` VALUES ('5', '计划', '2018-12-28 09:34:16', null, '1');
INSERT INTO `article_tag` VALUES ('6', '提问', '2018-12-28 09:34:48', null, '1');
INSERT INTO `article_tag` VALUES ('7', '百科', '2018-12-28 09:35:01', null, '1');
INSERT INTO `article_tag` VALUES ('8', '图片', '2018-12-28 09:35:17', null, '1');
INSERT INTO `article_tag` VALUES ('9', '科技', '2018-12-28 09:35:25', null, '1');
INSERT INTO `article_tag` VALUES ('10', '体育', '2018-12-28 09:35:32', null, '1');
INSERT INTO `article_tag` VALUES ('11', '娱乐', '2018-12-28 09:35:42', null, '1');

-- ----------------------------
-- Table structure for article_type
-- ----------------------------
DROP TABLE IF EXISTS `article_type`;
CREATE TABLE `article_type` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `typeName` varchar(50) DEFAULT NULL,
  `createTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updateTime` datetime DEFAULT NULL,
  `validFlag` int(11) NOT NULL DEFAULT '1',
  `icon` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='文章类型';

-- ----------------------------
-- Records of article_type
-- ----------------------------
INSERT INTO `article_type` VALUES ('1', '随笔', '2018-12-27 14:24:27', null, '1', 'icon/post.png');
INSERT INTO `article_type` VALUES ('2', '短文', '2018-12-27 14:24:27', null, '1', 'icon/post.png');
INSERT INTO `article_type` VALUES ('3', '其他', '2018-12-27 14:24:27', null, '1', 'icon/post.png');

-- ----------------------------
-- Table structure for user_info
-- ----------------------------
DROP TABLE IF EXISTS `user_info`;
CREATE TABLE `user_info` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `userName` varchar(255) DEFAULT NULL,
  `displayName` varchar(255) DEFAULT NULL,
  `loginPwd` varchar(255) DEFAULT NULL,
  `channel` varchar(255) DEFAULT NULL,
  `email` varchar(255) DEFAULT NULL,
  `roleCode` varchar(255) DEFAULT NULL,
  `levelId` varchar(255) DEFAULT NULL,
  `createTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updateTime` datetime DEFAULT NULL,
  `validFlag` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='用户信息';

-- ----------------------------
-- Records of user_info
-- ----------------------------
